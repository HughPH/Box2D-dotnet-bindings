using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

struct ChainShapeId : IEquatable<ChainShapeId>, IComparable<ChainShapeId>
{
    public int index1;
    public ushort world0;
    public ushort generation;

    public ChainShapeId(int index1, ushort world0, ushort generation)
    {
        this.index1 = index1;
        this.world0 = world0;
        this.generation = generation;
    }
    public bool Equals(ChainShapeId other) =>
        index1 == other.index1 && world0 == other.world0 && generation == other.generation;
    public override bool Equals(object? obj) =>
        obj is ChainShapeId other && Equals(other);
    public override int GetHashCode() =>
        HashCode.Combine(index1, world0, generation);
    public int CompareTo(ChainShapeId other)
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

/// <summary>
/// A chain shape is a series of connected line segments.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public partial class ChainShape : IEquatable<ChainShape>, IComparable<ChainShape>
{
    internal ChainShapeId id;
    
    internal ChainShape(ChainShapeId id)
    {
        this.id = id;
        if (!chainShapes.ContainsKey(id))
            chainShapes[id] = this;
    }

    /// <summary>
    /// Destroys this chain shape
    /// </summary>
    /// <remarks>This will remove the chain shape from the world and destroy all contacts associated with this shape</remarks>
    public unsafe void Destroy()
    {
        if (!Valid) return;
        b2DestroyChain(id);
        chainShapes.Remove(id);
    }
    
    /// <summary>
    /// Gets the world that owns this chain shape
    /// </summary>
    /// <returns>The world that owns this chain shape</returns>
    public unsafe World World => Valid ? World.GetWorld(b2Chain_GetWorld(id)) : throw new InvalidOperationException("Chain shape is not valid");

    private Shape[]? segments = null;
    
    /// <summary>
    /// The chain segments
    /// </summary>
    public unsafe ReadOnlySpan<Shape> Segments
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");

            if (segments is null)
            {
                int needed = b2Chain_GetSegmentCount(id);
                Shape[] buffer =
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<Shape>(needed);
#else
                    new Shape[needed];
#endif
                int written;
                fixed (Shape* p = buffer)
                    written = b2Chain_GetSegments(id, p, buffer.Length);
                
                segments = buffer[..written];
            }
            return segments.AsSpan();
        }
    }
    
    /// <summary>
    /// The chain friction
    /// </summary>
    public unsafe float Friction
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetFriction(id);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetFriction(id, value);
        }
    }
    
    /// <summary>
    /// The chain restitution (bounciness)
    /// </summary>
    public unsafe float Restitution
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetRestitution(id);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetRestitution(id, value);
        }
    }
    
    /// <summary>
    /// The chain material
    /// </summary>
    public unsafe int Material
    {
        get => !Valid ? throw new InvalidOperationException("The chain shape is not valid.") : b2Chain_GetMaterial(id);
        set
        {
            if (!Valid)
                throw new InvalidOperationException("The chain shape is not valid.");
            b2Chain_SetMaterial(id, value);
        }
    }
    
    /// <summary>
    /// Checks if the chain shape is valid
    /// </summary>
    /// <returns>True if the chain shape is valid, false otherwise</returns>
    public unsafe bool Valid => b2Chain_IsValid(id) != 0;

    /// <summary>Checks equality between two <see cref="ChainShape"/> values.</summary>
    public bool Equals(ChainShape other) =>
        id.index1 == other.id.index1 && id.world0 == other.id.world0 && id.generation == other.id.generation;

    /// <inheritdoc/>
    public override bool Equals(object? obj) =>
        obj is ChainShape other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() =>
        HashCode.Combine(id.index1, id.world0, id.generation);

    /// <summary>Default equality comparer for <see cref="ChainShape"/>.</summary>
    public static IEqualityComparer<ChainShape> DefaultEqualityComparer { get; } = ChainShapeComparer.Instance;

    /// <summary>Default comparer for <see cref="ChainShape"/>.</summary>
    public static IComparer<ChainShape> DefaultComparer { get; } = ChainShapeComparer.Instance;

    /// <summary>Compares this instance to another <see cref="ChainShape"/>.</summary>
    public int CompareTo(ChainShape other)
    {
        int index1Comparison = id.index1.CompareTo(other.id.index1);
        if (index1Comparison != 0)
            return index1Comparison;
        int world0Comparison = id.world0.CompareTo(other.id.world0);
        if (world0Comparison != 0)
            return world0Comparison;
        return id.generation.CompareTo(other.id.generation);
    }
    
    private static Dictionary<ChainShapeId, ChainShape> chainShapes = new();
    
    internal static ChainShape GetChain(ChainShapeId id)
    {
        if (!chainShapes.TryGetValue(id, out ChainShape? chainShape))
            chainShapes[id] = chainShape = new ChainShape(id);
        return chainShape;
    }
}