namespace Box2D;

/// <summary>
/// These functions can be provided to Box2D to invoke a task system. These are designed to work well with enkiTS.<br/>
/// Returns a pointer to the user's task object. May be nullptr. A nullptr indicates to Box2D that the work was executed
/// serially within the callback and there is no need to call FinishTaskCallback.<br/>
/// The itemCount is the number of Box2D work items that are to be partitioned among workers by the user's task system.<br/>
/// This is essentially a parallel-for. The minRange parameter is a suggestion of the minimum number of items to assign
/// per worker to reduce overhead. For example, suppose the task is small and that itemCount is 16. A minRange of 8 suggests
/// that your task system should split the work items among just two workers, even if you have more available.<br/>
/// In general the range [startIndex, endIndex) send to b2TaskCallback should obey:
/// <code>endIndex - startIndex &gt;= minRange</code><br/>
/// The exception of course is when itemCount &lt; minRange.
/// </summary>
/// <param name="task">The task callback</param>
/// <param name="itemCount">The number of Box2D work items</param>
/// <param name="minRange">The minimum range</param>
/// <param name="taskContext">The task context</param>
/// <param name="userContext">The user context</param>
/// <returns>A pointer to the user's task object</returns>
/// <remarks>
/// task is passed as a `nint` because the CLR will otherwise keep calling Marshal.GetDelegateForFunctionPointer, causing heap allocations. On the Box2D side, it's a consistent `void*` pointer, so we can cache it. See <see cref="Parallelism.DefaultEnqueue"/> source for how we handle this.
/// </remarks>
public delegate nint EnqueueTaskCallback(nint task, int itemCount, int minRange, nint taskContext, nint userContext);
