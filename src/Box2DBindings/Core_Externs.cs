using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Box2D
{
    partial class Core
    {
#if NET5_0_OR_GREATER
        /// <summary>
        /// Get the current version of Box2D
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<Box2DVersion> GetVersion;

        /// <summary>
        /// Get the absolute number of system ticks. The value is platform specific.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<ulong> GetTicks;

        /// <summary>
        /// Get the milliseconds passed from an initial tick value.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<ulong, float> GetMilliseconds;

        /// <summary>
        /// Get the milliseconds passed from an initial tick value. Resets the passed in
        /// value to the current tick value.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<ref ulong, float> GetMillisecondsAndReset;

        /// <summary>
        /// Yield to be used in a busy loop.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<void> Yield;

        /// <summary>
        /// Simple djb2 hash function for determinism testing
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<uint, byte[], int, uint> Hash;

        private static readonly unsafe delegate* unmanaged[Cdecl]<in Vec2, in Vec2, in Vec2, in Vec2, SegmentDistanceResult> b2SegmentDistance;

        private static readonly unsafe delegate* unmanaged[Cdecl]<in DistanceInput, ref SimplexCache, Simplex*, int, DistanceOutput> b2ShapeDistance;

        /// <summary>
        /// Perform a linear shape cast of shape B moving and shape A fixed. Determines the hit point, normal, and translation fraction.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<in ShapeCastPairInput, CastOutput> ShapeCast;

        private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2*, int, float, ShapeProxy> b2MakeProxy;

        /// <summary>
        /// Compute the upper bound on time before two shapes penetrate. Time is represented as
        /// a fraction between [0,tMax]. This uses a swept separating axis and may miss some intermediate,
        /// non-tunneling collisions. If you change the time interval, you should call this function
        /// again.
        /// </summary>
        public static readonly unsafe delegate* unmanaged[Cdecl]<in TOIInput, TOIOutput> TimeOfImpact;

        private static readonly unsafe delegate* unmanaged[Cdecl]<float, void> b2SetLengthUnitsPerMeter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float> b2GetLengthUnitsPerMeter;

        private static readonly unsafe delegate* unmanaged[Cdecl]<nint, void> b2SetAssertFcn;

        static unsafe Core()
        {
            var lib = NativeLibrary.Load(libraryName);

            NativeLibrary.TryGetExport(lib, "b2GetVersion", out var p0);
            NativeLibrary.TryGetExport(lib, "b2GetTicks", out var p1);
            NativeLibrary.TryGetExport(lib, "b2GetMilliseconds", out var p2);
            NativeLibrary.TryGetExport(lib, "b2GetMillisecondsAndReset", out var p3);
            NativeLibrary.TryGetExport(lib, "b2Yield", out var p4);
            NativeLibrary.TryGetExport(lib, "b2Hash", out var p5);
            NativeLibrary.TryGetExport(lib, "b2SegmentDistance", out var p6);
            NativeLibrary.TryGetExport(lib, "b2ShapeDistance", out var p7);
            NativeLibrary.TryGetExport(lib, "b2ShapeCast", out var p8);
            NativeLibrary.TryGetExport(lib, "b2MakeProxy", out var p9);
            NativeLibrary.TryGetExport(lib, "b2TimeOfImpact", out var p10);
            NativeLibrary.TryGetExport(lib, "b2SetLengthUnitsPerMeter", out var p11);
            NativeLibrary.TryGetExport(lib, "b2GetLengthUnitsPerMeter", out var p12);
            NativeLibrary.TryGetExport(lib, "b2SetAssertFcn", out var p13);

            GetVersion = (delegate* unmanaged[Cdecl]<Box2DVersion>)p0;
            GetTicks = (delegate* unmanaged[Cdecl]<ulong>)p1;
            GetMilliseconds = (delegate* unmanaged[Cdecl]<ulong, float>)p2;
            GetMillisecondsAndReset = (delegate* unmanaged[Cdecl]<ref ulong, float>)p3;
            Yield = (delegate* unmanaged[Cdecl]<void>)p4;
            Hash = (delegate* unmanaged[Cdecl]<uint, byte[], int, uint>)p5;
            b2SegmentDistance = (delegate* unmanaged[Cdecl]<in Vec2, in Vec2, in Vec2, in Vec2, SegmentDistanceResult>)p6;
            b2ShapeDistance = (delegate* unmanaged[Cdecl]<in DistanceInput, ref SimplexCache, Simplex*, int, DistanceOutput>)p7;
            ShapeCast = (delegate* unmanaged[Cdecl]<in ShapeCastPairInput, CastOutput>)p8;
            b2MakeProxy = (delegate* unmanaged[Cdecl]<Vec2*, int, float, ShapeProxy>)p9;
            TimeOfImpact = (delegate* unmanaged[Cdecl]<in TOIInput, TOIOutput>)p10;
            b2SetLengthUnitsPerMeter = (delegate* unmanaged[Cdecl]<float, void>)p11;
            b2GetLengthUnitsPerMeter = (delegate* unmanaged[Cdecl]<float>)p12;
            b2SetAssertFcn = (delegate* unmanaged[Cdecl]<nint, void>)p13;
        }
        
        /// <summary>
        /// Compute the distance between two line segments, clamping at the end points if needed.
        /// </summary>
        public static unsafe SegmentDistanceResult SegmentDistance(in Vec2 p1, in Vec2 q1, in Vec2 p2, in Vec2 q2) => b2SegmentDistance(p1, q1, p2, q2);
    #else 

    /// <summary>
    /// Get the current version of Box2D
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetVersion")]
    public static extern Box2DVersion GetVersion();
    
    /// <summary>
    /// Get the absolute number of system ticks. The value is platform specific.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetTicks")]
    public static extern ulong GetTicks();
    
    /// <summary>
    /// Get the milliseconds passed from an initial tick value.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetMilliseconds")]
    public static extern float GetMilliseconds(ulong ticks);
    
    /// <summary>
    /// Get the milliseconds passed from an initial tick value. Resets the passed in
    /// value to the current tick value.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetMillisecondsAndReset")]
    public static extern float GetMillisecondsAndReset(ref ulong ticks);
    
    /// <summary>
    /// Yield to be used in a busy loop.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Yield")]
    public static extern void Yield();

    /// <summary>
    /// Simple djb2 hash function for determinism testing
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Hash")]
    public static extern uint Hash(uint hash, byte[] data, int count);
    
    /// <summary>
    /// Compute the distance between two line segments, clamping at the end points if needed.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SegmentDistance")]
    public static extern SegmentDistanceResult SegmentDistance(in Vec2 p1, in Vec2 q1, in Vec2 p2, in Vec2 q2);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeDistance")]
    private static extern unsafe DistanceOutput b2ShapeDistance(in DistanceInput input, ref SimplexCache cache, Simplex* simplexes, int simplexCapacity);

    /// <summary>
    /// Perform a linear shape cast of shape B moving and shape A fixed. Determines the hit point, normal, and translation fraction.
    /// </summary>
    /// <remarks>
    /// Initially touching shapes are treated as a miss.
    /// </remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCast")]
    public static extern CastOutput ShapeCast(in ShapeCastPairInput input);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeProxy")]
    private static extern unsafe ShapeProxy b2MakeProxy(Vec2* points, int count, float radius);
    
    /// <summary>
    /// Compute the upper bound on time before two shapes penetrate. Time is represented as
    /// a fraction between [0,tMax]. This uses a swept separating axis and may miss some intermediate,
    /// non-tunneling collisions. If you change the time interval, you should call this function
    /// again.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2TimeOfImpact")]
    public static extern TOIOutput TimeOfImpact(in TOIInput input);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SetLengthUnitsPerMeter")]
    private static extern void b2SetLengthUnitsPerMeter(float lengthUnitsPerMeter);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetLengthUnitsPerMeter")]
    private static extern float b2GetLengthUnitsPerMeter();

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SetAssertFcn")]
    private static extern void b2SetAssertFcn(nint assertFcn);
#endif
    }
}
