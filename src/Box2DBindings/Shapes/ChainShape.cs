using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A chain shape is a series of connected line segments.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct ChainShape : IEquatable<ChainShape>, IComparable<ChainShape>
{
    private int index1;
    private ushort world0;
    private ushort generation;
    
    /// <summary>
    /// Create a ChainShape on the specified body with the specified definition.
    /// </summary>
    /// <param name="body">The body on which to create the shape</param>
    /// <param name="def">The ChainDef definition</param>
    public ChainShape(Body body, ChainDef def)
    {
        this = body.CreateChain(def);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyChain")]
    private static extern void b2DestroyChain(ChainShape chainId);
    
    /// <summary>
    /// Destroys this chain shape
    /// </summary>
    /// <remarks>This will remove the chain shape from the world and destroy all contacts associated with this shape</remarks>
    public void Destroy()
    {
        if (!Valid) return;
        b2DestroyChain(this);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetWorld")]
    private static extern WorldId b2Chain_GetWorld(ChainShape chainId);
    
    /// <summary>
    /// Gets the world that owns this chain shape
    /// </summary>
    /// <returns>The world that owns this chain shape</returns>
    public World World => Valid ? World.GetWorld(b2Chain_GetWorld(this)) : throw new InvalidOperationException("Chain shape is not valid");
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegmentCount")]
    private static extern int b2Chain_GetSegmentCount(ChainShape chainId);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegments")]
    private static extern unsafe int b2Chain_GetSegments(ChainShape chainId, [In] Shape* segmentArray, int capacity);
    
    /// <summary>
    /// The chain segments
    /// </summary>
    public unsafe ReadOnlySpan<Shape> Segments
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            int needed = b2Chain_GetSegmentCount(this);
            Shape[] buffer = 
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<Shape>(needed);
#else
                new Shape[needed];
#endif
            int written;
            fixed (Shape* p = buffer)
                written = b2Chain_GetSegments(this, p, buffer.Length);
            return buffer.AsSpan(0, written);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetFriction")]
    private static extern void b2Chain_SetFriction(ChainShape chainId, float friction);
   
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetFriction")]
    private static extern float b2Chain_GetFriction(ChainShape chainId);
    
    /// <summary>
    /// The chain friction
    /// </summary>
    public float Friction
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetFriction(this);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetFriction(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetRestitution")]
    private static extern void b2Chain_SetRestitution(ChainShape chainId, float restitution);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetRestitution")]
    private static extern float b2Chain_GetRestitution(ChainShape chainId);
    
    /// <summary>
    /// The chain restitution (bounciness)
    /// </summary>
    public float Restitution
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetRestitution(this);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetRestitution(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetMaterial")]
    private static extern void b2Chain_SetMaterial(ChainShape chainId, int material);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetMaterial")]
    private static extern int b2Chain_GetMaterial(ChainShape chainId);
    
    /// <summary>
    /// The chain material
    /// </summary>
    public int Material
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetMaterial(this);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetMaterial(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_IsValid")]
    private static extern byte b2Chain_IsValid(ChainShape chainId);
    
    /// <summary>
    /// Checks if the chain shape is valid
    /// </summary>
    /// <returns>True if the chain shape is valid, false otherwise</returns>
    public bool Valid => b2Chain_IsValid(this) != 0;

    /// <summary>Checks equality between two <see cref="ChainShape"/> values.</summary>
    public bool Equals(ChainShape other) =>
        index1 == other.index1 && world0 == other.world0 && generation == other.generation;

    /// <inheritdoc/>
    public override bool Equals(object? obj) =>
        obj is ChainShape other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() =>
        HashCode.Combine(index1, world0, generation);

    /// <summary>Default equality comparer for <see cref="ChainShape"/>.</summary>
    public static IEqualityComparer<ChainShape> DefaultEqualityComparer { get; } = ChainShapeComparer.Instance;

    /// <summary>Default comparer for <see cref="ChainShape"/>.</summary>
    public static IComparer<ChainShape> DefaultComparer { get; } = ChainShapeComparer.Instance;

    /// <summary>Compares this instance to another <see cref="ChainShape"/>.</summary>
    public int CompareTo(ChainShape other)
    {
        int index1Comparison = index1.CompareTo(other.index1);
        if (index1Comparison != 0)
            return index1Comparison;
        int world0Comparison = world0.CompareTo(other.world0);
        if (world0Comparison != 0)
            return world0Comparison;
        return generation.CompareTo(other.generation);
    }
}