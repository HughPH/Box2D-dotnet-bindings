using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)] // The alternative to LayoutKind.Explicit is to have two padding bytes between AllowFastRotation and internalValue
struct BodyDefInternal
{
    [FieldOffset(0)]
    internal BodyType Type;

    [FieldOffset(4)]
    internal Vec2 Position;

    [FieldOffset(12)]
    internal Rotation Rotation;

    [FieldOffset(20)]
    internal Vec2 LinearVelocity;

    [FieldOffset(28)]
    internal float AngularVelocity;

    [FieldOffset(32)]
    internal float LinearDamping;

    [FieldOffset(36)]
    internal float AngularDamping;

    [FieldOffset(40)]
    internal float GravityScale;

    [FieldOffset(44)]
    internal float SleepThreshold;
    
    [FieldOffset(48)]
    internal nint Name;
	
    [FieldOffset(56)]
    internal nint UserData;

    [FieldOffset(64)]
    internal byte EnableSleep;

    [FieldOffset(65)]
    internal byte IsAwake;

    [FieldOffset(66)]
    internal byte FixedRotation;

    [FieldOffset(67)]
    internal byte IsBullet;

    [FieldOffset(68)]
    internal byte IsEnabled;

    [FieldOffset(69)]
    internal byte AllowFastRotation;

    [FieldOffset(72)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultBodyDef")]
    private static extern BodyDefInternal GetDefault();
    
    private static BodyDefInternal Default => GetDefault();

    public BodyDefInternal()
    {
        this = Default;
    }
}