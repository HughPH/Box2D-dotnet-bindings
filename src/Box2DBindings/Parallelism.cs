using System;
using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;
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
    private static int maxWorkerCount = Math.Max(1, Environment.ProcessorCount / 2);
#if NET9_0_OR_GREATER
    private static readonly Lock Sync = new();
#else
    private static readonly object Sync = new();
#endif
    private static readonly ConcurrentDictionary<nint, TaskCallback> TaskCache = new();
    private static readonly ConcurrentQueue<nint> TaskCacheFifo = new();
    private const int TaskCacheMaxEntries = 1024;
    private const int TaskCachePruneBatch = 64;
    private static int taskCacheCount;
    private static int taskCachePruning;

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
            lock (Sync)
            {
                if (World.worlds.Count > 0)
                    throw new InvalidOperationException("Cannot change thread count while worlds exist.");

                maxWorkerCount = Math.Clamp(value, 1, Environment.ProcessorCount);
            }
        }
    }

    private sealed class Job
    {
        public TaskCallback Task;
        public int Start, End;
        public uint Index;
        public nint TaskContext;
        public BatchState Batch;

        public void Execute()
        {
            try
            {
                Task(Start, End, Index, TaskContext);
            }
            catch (Exception ex)
            {
                Interlocked.CompareExchange(ref Batch.Exception, ex, null);
            }
            finally
            {
                Batch.Countdown.Signal();
            }
        }
    }

    private sealed class BatchState
    {
        public CountdownEvent Countdown = new(1);
        public Exception? Exception;
    }

    internal static nint DefaultEnqueue(nint taskPtr, int itemCount, int minRange, nint taskContext, nint userContext)
    {
        var task = GetOrAddTaskCallback(taskPtr);

        if (maxWorkerCount <= 1 || itemCount <= minRange)
        {
            task(0, itemCount, 0u, taskContext);
            return 0;
        }

        int chunk = Math.Max(minRange, (itemCount + maxWorkerCount - 1) / maxWorkerCount);
        var batch = new BatchState();

        for (int w = 0; w < maxWorkerCount; w++)
        {
            int start = w * chunk;
            int end = Math.Min(itemCount, start + chunk);
            if (start >= end) break;

            batch.Countdown.AddCount(); // Call *before* enqueueing
            ThreadPool.QueueUserWorkItem(static job => job.Execute(), new Job
            {
                Task = task,
                Start = start,
                End = end,
                Index = (uint)w,
                TaskContext = taskContext,
                Batch = batch
            }, preferLocal: false);
        }

        batch.Countdown.Signal(); // main thread's count

        var handle = GCHandle.Alloc(batch, GCHandleType.Normal);
        return GCHandle.ToIntPtr(handle);
    }

    private static TaskCallback GetOrAddTaskCallback(nint taskPtr)
    {
        if (TaskCache.TryGetValue(taskPtr, out var existing))
            return existing;

        var created = Marshal.GetDelegateForFunctionPointer<TaskCallback>(taskPtr);
        if (TaskCache.TryAdd(taskPtr, created))
        {
            TaskCacheFifo.Enqueue(taskPtr);
            int count = Interlocked.Increment(ref taskCacheCount);
            if (count > TaskCacheMaxEntries)
                TryPruneTaskCacheFifo();
            return created;
        }

        TaskCache.TryGetValue(taskPtr, out var raced);
        return raced!;
    }

    private static void TryPruneTaskCacheFifo()
    {
        // One concurrent pruner is enough; eviction is best-effort.
        if (Interlocked.Exchange(ref taskCachePruning, 1) != 0)
            return;

        try
        {
            int target = Math.Max(0, TaskCacheMaxEntries - TaskCachePruneBatch);
            int removed = 0;

            while (Volatile.Read(ref taskCacheCount) > target && removed < TaskCachePruneBatch && TaskCacheFifo.TryDequeue(out var key))
            {
                if (TaskCache.TryRemove(key, out _))
                {
                    Interlocked.Decrement(ref taskCacheCount);
                    removed++;
                }
            }
        }
        finally
        {
            Volatile.Write(ref taskCachePruning, 0);
        }
    }

    internal static void DefaultFinish(nint userTask, nint userContext)
    {
        if (userTask == 0)
            return;

        var handle = GCHandle.FromIntPtr(userTask);
        var batch = (BatchState)handle.Target!;
        Exception? captured;

        try
        {
            batch.Countdown.Wait();
            captured = batch.Exception;
        }
        finally
        {
            batch.Countdown.Dispose();
            handle.Free();
        }

        if (captured != null)
            ExceptionDispatchInfo.Capture(captured).Throw();
    }
}