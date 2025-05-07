using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

[PublicAPI]
public static class Stats
{
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetByteCount")]
    private static extern int GetByteCount();
    
    /// <summary>
    /// Get the number of bytes allocated by Box2D
    /// </summary>
    /// <returns>The number of bytes allocated by Box2D</returns>
    public static int GetAllocatedBytes() => GetByteCount();

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
