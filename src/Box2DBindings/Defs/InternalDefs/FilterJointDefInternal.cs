using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A null joint is used to disable collision between two specific bodies.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
struct FilterJointDefInternal
{
    /// <summary>
    /// The first attached body.
    /// </summary>
    internal Body BodyA;
    
    /// <summary>
    /// The second attached body.
    /// </summary>
    internal Body BodyB;

    /// <summary>
    /// User data pointer
    /// </summary>
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    private readonly int internalValue;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultNullJointDef")]
    private static extern FilterJointDefInternal GetDefault();
    
    /// <summary>
    /// The default null joint definition.
    /// </summary>
    private static FilterJointDefInternal Default => GetDefault();
    
    public FilterJointDefInternal()
    {
        this = Default;
    }
}