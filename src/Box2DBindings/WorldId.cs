using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
readonly struct WorldId : IEquatable<WorldId>, IComparable<WorldId>
{
    internal readonly ushort index1;
    internal readonly ushort generation;
        
    public bool Equals(WorldId other) =>
        index1 == other.index1 && generation == other.generation;
        
    public override bool Equals(object? obj) =>
        obj is WorldId other && Equals(other);
        
    public override int GetHashCode() =>
        HashCode.Combine(index1, generation);
        
    public static IEqualityComparer<WorldId> DefaultEqualityComparer { get; } = WorldIdComparer.Instance;
        
    public static IComparer<WorldId> DefaultComparer { get; } = WorldIdComparer.Instance;
    
    public int CompareTo(WorldId other)
    {
        int index1Comparison = index1.CompareTo(other.index1);
        if (index1Comparison != 0)
            return index1Comparison;
        return generation.CompareTo(other.generation);
    }
}