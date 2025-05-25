using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
struct BodyId : IEquatable<BodyId>, IComparable<BodyId>
{
    internal int index1;
    internal ushort world0;
    internal ushort generation;

    public BodyId(int index1, ushort world0, ushort generation)
    {
        this.index1 = index1;
        this.world0 = world0;
        this.generation = generation;
    }

    public bool Equals(BodyId other) => index1 == other.index1 && world0 == other.world0 && generation == other.generation;

    public override bool Equals(object? obj) => obj is BodyId other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(index1, world0, generation);

    public static IEqualityComparer<BodyId> DefaultEqualityComparer { get; } = EqualityComparer<BodyId>.Default;

    public static IComparer<BodyId> DefaultComparer { get; } = Comparer<BodyId>.Default;

    public int CompareTo(BodyId other)
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
