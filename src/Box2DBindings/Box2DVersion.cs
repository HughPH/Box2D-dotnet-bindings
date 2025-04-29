using System.Runtime.InteropServices;
/// <summary>
/// Box2D version information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Box2DVersion
{
    /// <summary>
    /// Significant changes
    /// </summary>
    public readonly int Major;

    /// <summary>
    /// Incremental changes
    /// </summary>
    public readonly int Minor;

    /// <summary>
    /// Bug fixes
    /// </summary>
    public readonly int Revision;
}
