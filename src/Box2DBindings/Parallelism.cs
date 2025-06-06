using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Parallelism class, manages worker threads for parallel task execution.
/// </summary>
/// <remarks>
/// The only public member is <see cref="MaxWorkerCount"/>. This is the maximum number of worker threads that can be used, which defaults to Environment.ProcessorCount / 2. It will be clamped to the number of logical processors available on the system. It cannot be changed while worlds exist.
/// </remarks>
[PublicAPI]
public static class Parallelism
{
    private static int maxWorkerCount = Environment.ProcessorCount / 2;
    private static BlockingCollection<Job>? jobQueue;
    private static Thread[]? workers;
    private static readonly ConcurrentDictionary<nint, TaskCallback> TaskCache = new();

    /// <summary>
    /// Maximum number of worker threads to use for parallel task execution.
    /// </summary>
    /// <remarks>
    /// This is the maximum number of worker threads that can be used, defaults to Environment.ProcessorCount / 2. It will be clamped to the number of logical processors available on the system. It cannot be changed while worlds exist.
    /// </remarks>
    public static int MaxWorkerCount
    {
        get => maxWorkerCount;
        set
        {
            if (World.worlds.Count > 0)
                throw new InvalidOperationException("Cannot change thread count while worlds exist.");
            maxWorkerCount = Math.Min(Math.Max(1, value), Environment.ProcessorCount);
            //terminate old threads
            if (workers != null)
            {
                foreach (var worker in workers)
                {
                    worker.Interrupt();
                    worker.Join();
                }
            }
            //create new threads
            InitWorkerThreads();
        }
    }

    static Parallelism()
    {
        InitWorkerThreads();
    }

    private static void InitWorkerThreads()
    {
        jobQueue = new BlockingCollection<Job>();
        workers = new Thread[maxWorkerCount];
        for (int i = 0; i < maxWorkerCount; i++)
        {
            workers[i] = new Thread(() =>
            {
                try
                {
                    foreach (var job in jobQueue!.GetConsumingEnumerable())
                        job.Execute();
                }catch (ThreadInterruptedException)
                {
                    // Thread was interrupted, exit gracefully
                }
                catch (Exception ex)
                {
                    // Handle exceptions from worker threads
                    Console.WriteLine($"Worker thread exception: {ex}");
                }
            }) { IsBackground = true };
            workers[i].Start();
        }
    }

    private struct Job
    {
        public TaskCallback Task;
        public int Start, End;
        public uint Index;
        public nint TaskContext;
        public CountdownEvent Countdown;

        public void Execute()
        {
            try
            {
                Task(Start, End, Index, TaskContext);
            }
            finally
            {
                Countdown.Signal();
            }
        }
    }

    internal static nint DefaultEnqueue(nint taskPtr, int itemCount, int minRange, nint taskContext, nint userContext)
    {
        if (!TaskCache.TryGetValue(taskPtr, out var task))
        {
            task = Marshal.GetDelegateForFunctionPointer<TaskCallback>(taskPtr);
            TaskCache[taskPtr] = task;
        }

        if (maxWorkerCount <= 1 || itemCount <= minRange)
        {
            task(0, itemCount, 0u, taskContext);
            return 0;
        }

        int chunk = Math.Max(minRange, (itemCount + maxWorkerCount - 1) / maxWorkerCount);
        var countdown = CountdownEventPool.Rent(1); // Pooled

        for (int w = 0; w < maxWorkerCount; w++)
        {
            int start = w * chunk;
            int end = Math.Min(itemCount, start + chunk);
            if (start >= end) break;

            countdown.AddCount(); // Call *before* enqueueing
            jobQueue!.Add(new Job
            {
                Task = task,
                Start = start,
                End = end,
                Index = (uint)w,
                TaskContext = taskContext,
                Countdown = countdown
            });
        }

        countdown.Signal(); // main thread's count

        var handle = GCHandle.Alloc(countdown, GCHandleType.Normal);
        return GCHandle.ToIntPtr(handle);
    }

    internal static void DefaultFinish(nint userTask, nint userContext)
    {
        if (userTask == 0)
            return;

        var handle = GCHandle.FromIntPtr(userTask);
        var countdown = (CountdownEvent)handle.Target!;
        countdown.Wait();
        CountdownEventPool.Return(countdown); // Return to pool
        handle.Free();
    }
    
    static class CountdownEventPool
    {
        private static readonly ConcurrentBag<CountdownEvent> pool = new();
        private const int MaxPoolSize = 64;
        private static int count;

        public static CountdownEvent Rent(int initialCount)
        {
            if (pool.TryTake(out var item))
            {
                Interlocked.Decrement(ref count);
                item.Reset(initialCount);
                return item;
            }

            return new CountdownEvent(initialCount);
        }

        public static void Return(CountdownEvent item)
        {
            if (Interlocked.Increment(ref count) > MaxPoolSize)
            {
                Interlocked.Decrement(ref count);
                item.Dispose();
                return;
            }

            pool.Add(item);
        }
    }
}