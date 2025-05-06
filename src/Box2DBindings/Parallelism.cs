using JetBrains.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Parallel config for Box2D.
/// </summary>
[PublicAPI]
public static class Parallelism
{
    private static int maxWorkerCount = Environment.ProcessorCount / 2;

    /// <summary>
    /// The maximum number of worker threads to use for parallel tasks.
    /// </summary>
    /// <remarks>
    /// This value is set to half the number of logical processors by default.<br/>
    /// <b>Warning: This value cannot be changed after a world has been created.</b><br/>
    /// <i>Note: If you want to supply your own task system, set WorldDef.MaxWorkers to your preferred value and set your own EnqueueTask and FinishTask callbacks.</i>
    /// </remarks>
    public static int MaxWorkerCount
    {
        get => maxWorkerCount;
        set
        {
            if (World.worlds.Any())
            {
                throw new InvalidOperationException(
                    "Cannot change the number of worker threads after a world has been created.");
            }
            maxWorkerCount = Math.Max(1, value);
        }
    }

    internal static nint DefaultEnqueue(
        TaskCallback task,
        int itemCount,
        int minRange,
        nint taskContext,
        nint userContext)
    {
        int workerCount = maxWorkerCount;
        if (workerCount <= 1 || itemCount <= minRange)
        {
            task(0, itemCount, 0u, taskContext);
            return IntPtr.Zero;
        }

        int chunk = Math.Max(minRange, (itemCount + workerCount - 1) / workerCount);
        var tasks = new Task[workerCount];
        int actualWorkers = 0;

        for (int w = 0; w < workerCount; w++)
        {
            int start = w * chunk;
            int end = Math.Min(itemCount, start + chunk);
            if (start >= end) break;

            int wi = w; // avoid access to modified closure
            tasks[actualWorkers++] = Task.Run(() =>
                    task(start, end, (uint)wi, taskContext)
                );
        }

        if (actualWorkers == 1)
        {
            tasks[0].Wait();
            return IntPtr.Zero;
        }

        if (actualWorkers != tasks.Length)
            Array.Resize(ref tasks, actualWorkers);

        var gch = GCHandle.Alloc(tasks);
        return GCHandle.ToIntPtr(gch);
    }

    internal static void DefaultFinish(nint userTask, nint userContext)
    {
        if (userTask == IntPtr.Zero)
            return;

        var gch = GCHandle.FromIntPtr(userTask);
        var tasks = (Task[])gch.Target;

        Task.WaitAll(tasks);

        gch.Free();
    }
}
