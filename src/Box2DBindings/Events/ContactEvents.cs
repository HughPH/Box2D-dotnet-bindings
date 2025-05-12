using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Contact events are buffered in the Box2D world and are available
/// as event arrays after the time step is complete.
/// <i>Note: these may become invalid if bodies and/or shapes are destroyed</i>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public readonly unsafe struct ContactEvents
{
    internal readonly ContactBeginTouchEvent* beginEvents;
	
    /// <summary>
    /// Array of begin touch events
    /// </summary>
    public ReadOnlySpan<ContactBeginTouchEvent> BeginEvents => new(beginEvents, beginCount);

    internal readonly ContactEndTouchEvent* endEvents;
	
    /// <summary>
    /// Array of end touch events
    /// </summary>
    public ReadOnlySpan<ContactEndTouchEvent> EndEvents => new(endEvents, endCount);

    internal readonly ContactHitEvent* hitEvents;
	
    /// <summary>
    /// Array of hit events
    /// </summary>
    public ReadOnlySpan<ContactHitEvent> HitEvents => new(hitEvents, hitCount);

    /// Number of begin touch events
    internal readonly int beginCount;

    /// Number of end touch events
    internal readonly int endCount;

    /// Number of hit events
    internal readonly int hitCount;
}