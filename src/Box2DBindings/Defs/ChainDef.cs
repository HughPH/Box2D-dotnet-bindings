using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Used to create a chain of line segments. This is designed to eliminate ghost collisions with some limitations.
/// <ul>
/// <li>chains are one-sided</li>
/// <li>chains have no mass and should be used on static bodies</li>
/// <li>chains have a counter-clockwise winding order</li>
/// <li>chains are either a loop or open</li>
/// <li>a chain must have at least 4 points</li>
/// <li>the distance between any two points must be greater than B2_LINEAR_SLOP</li>
/// <li>a chain shape should not self intersect (this is not validated)</li>
/// <li>an open chain shape has NO COLLISION on the first and final edge</li>
/// <li>you may overlap two open chains on their first three and/or last three points to get smooth collision</li>
/// <li>a chain shape creates multiple line segment shapes on the body</li>
/// </ul>
/// https://en.wikipedia.org/wiki/Polygonal_chain<br/>
/// <b>Warning: Do not use chain shapes unless you understand the limitations. This is an advanced feature.</b>
/// </summary>
[PublicAPI]
public class ChainDef
{
    internal ChainDefInternal _internal;

    /// <summary>
    /// Creates a chain definition with the default values.
    /// </summary>
    public ChainDef()
    {
        _internal = new();
    }

    /// <summary>
    /// Destructor for ChainDef.
    /// </summary>
    unsafe ~ChainDef()
    {
        // Free the points and materials if they were allocated
        if ((nint)_internal.Points != 0)
        {
            Marshal.FreeHGlobal((nint)_internal.Points);
            _internal.Points =(Vec2*)0; 
            _internal.Count = 0;
        }
        if ((nint)_internal.Materials != 0 && materialsAllocated)
        {
            Marshal.FreeHGlobal((nint)_internal.Materials);
            _internal.Materials = (SurfaceMaterial*)0;
            _internal.MaterialCount = 0;
        }
    }

    /// <summary>
    /// Use this to store application specific shape data.
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }

    /// <summary>
    /// An array of at least 4 points. These are cloned and may be temporary.
    /// </summary> 
    public unsafe Span<Vec2> Points
    {
        get
        {
            if (_internal.Points == null)
                return Span<Vec2>.Empty;
            return new(_internal.Points, _internal.Count);
        }
        set
        {
            if (_internal.Points != null)
            {
                Marshal.FreeHGlobal((nint)_internal.Points);
                _internal.Points = (Vec2*)0;
                _internal.Count = 0;
            }
            if (value.Length > 0)
            {
                _internal.Points = (Vec2*)Marshal.AllocHGlobal(value.Length * sizeof(Vec2));
                for (int i = 0; i < value.Length; i++)
                    _internal.Points[i] = value[i];
                _internal.Count = value.Length;
            }
        }
    }

    private bool materialsAllocated;
    
    /// <summary>
    /// Surface materials for each segment. These are cloned.
    /// </summary>
    public unsafe Span<SurfaceMaterial> Materials
    {
        get
        {
            if (_internal.Materials == null)
                return Span<SurfaceMaterial>.Empty;
            return new(_internal.Materials, _internal.MaterialCount);
        }
        set
        {
            if (_internal.Materials != null && materialsAllocated)
            {
                Marshal.FreeHGlobal((nint)_internal.Materials);
                _internal.Materials = (SurfaceMaterial*)0;
                _internal.MaterialCount = 0;
            }
            if (value.Length > 0)
            {
                _internal.Materials = (SurfaceMaterial*)Marshal.AllocHGlobal(value.Length * sizeof(SurfaceMaterial));
                for (int i = 0; i < value.Length; i++)
                    _internal.Materials[i] = value[i];
                _internal.MaterialCount = value.Length;
                materialsAllocated = true;
            }
        }
    }
    
    /// <summary>
    /// Contact filtering data.
    /// </summary>
    public ref Filter Filter => ref _internal.Filter;

    /// <summary>
    /// Indicates a closed chain formed by connecting the first and last points
    /// </summary>
    public bool IsLoop
    {
        get => _internal.IsLoop != 0;
        set => _internal.IsLoop = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Enable sensors to detect this chain. False by default.
    /// </summary>
    public bool EnableSensorEvents 
    {
        get => _internal.EnableSensorEvents != 0;
        set => _internal.EnableSensorEvents = value ? (byte)1 : (byte)0;
    }
}