using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)] // The alternative to LayoutKind.Explicit is to have two padding bytes between AllowFastRotation and internalValue
struct BodyDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<BodyDefInternal> b2DefaultBodyDef;

    static unsafe BodyDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultBodyDef", out var ptr);
        b2DefaultBodyDef = (delegate* unmanaged[Cdecl]<BodyDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultBodyDef")]
    private static extern BodyDefInternal b2DefaultBodyDef();
#endif
    
    internal BodyType Type;

    internal Vec2 Position;

    internal Rotation Rotation;

    internal Vec2 LinearVelocity;

    internal float AngularVelocity;

    internal float LinearDamping;

    internal float AngularDamping;

    internal float GravityScale;

    internal float SleepThreshold;
    
    internal nint Name;
	
    internal nint UserData;

    internal byte EnableSleep;

    internal byte IsAwake;

    internal byte FixedRotation;

    internal byte IsBullet;

    internal byte IsEnabled;

    internal byte AllowFastRotation;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    private static unsafe BodyDefInternal Default => b2DefaultBodyDef();

    public BodyDefInternal()
    {
        this = Default;
    }
}