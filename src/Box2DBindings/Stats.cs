using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Utility functions for collecting profiling and memory statistics from Box2D.
/// </summary>
[PublicAPI]
public static class Stats
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<int> b2GetByteCount;

    static unsafe Stats()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2GetByteCount", out var ptr);
        b2GetByteCount = (delegate* unmanaged[Cdecl]<int>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetByteCount")]
    private static extern int b2GetByteCount();
#endif
    
    /// <summary>
    /// Get the number of bytes allocated by Box2D
    /// </summary>
    /// <returns>The number of bytes allocated by Box2D</returns>
    public static unsafe int GetAllocatedBytes() => b2GetByteCount();

    /// <summary>
    /// Get the world performance profile for the supplied world
    /// </summary>
    public static Profile GetWorldProfile(World world) => world.Profile;

    /// <summary>
    /// Get counters and sizes for the supplied world
    /// </summary>
    public static Counters GetWorldCounters(World world) => world.Counters;

    /// <summary>
    /// Dump world memory stats to box2d_memory.txt
    /// </summary>
    public static void DumpMemoryStats(World world) => world.DumpMemoryStats();
}
