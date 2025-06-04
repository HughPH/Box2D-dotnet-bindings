using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct WorldDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldDefInternal> b2DefaultWorldDef;

    static unsafe WorldDefInternal()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2DefaultWorldDef", out var ptr);
        b2DefaultWorldDef = (delegate* unmanaged[Cdecl]<WorldDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWorldDef")]
    private static extern WorldDefInternal b2DefaultWorldDef();
#endif

    [FieldOffset(0)]
    internal Vec2 Gravity;

    [FieldOffset(8)]
    internal float RestitutionThreshold;

    [FieldOffset(12)]
    internal float HitEventThreshold;

    [FieldOffset(16)]
    internal float ContactHertz;

    [FieldOffset(20)]
    internal float ContactDampingRatio;

    [FieldOffset(24)]
    internal float MaxContactPushSpeed;
    
    [FieldOffset(28)]
    internal float MaximumLinearSpeed;

    [FieldOffset(32)]
    internal FrictionCallback FrictionCallback;

    [FieldOffset(40)]
    internal RestitutionCallback RestitutionCallback;

    [FieldOffset(48)]
    internal byte EnableSleep;

    [FieldOffset(49)]
    internal byte EnableContinuous;

    [FieldOffset(52)]
    internal int WorkerCount;

    [FieldOffset(56)]
    internal EnqueueTaskCallback EnqueueTask;

    [FieldOffset(64)]
    internal FinishTaskCallback FinishTask;

    [FieldOffset(72)]
    internal nint UserTaskContext;

    [FieldOffset(80)]
    internal nint UserData;

    [FieldOffset(88)]
    private readonly int internalValue;
    
    private static unsafe WorldDefInternal Default => b2DefaultWorldDef();
    
    public WorldDefInternal()
    {
        this = Default;
    }
}