using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Box2D.Comparers;

sealed class WorldComparer : IEqualityComparer<World>, IComparer<World>
{
    public static readonly WorldComparer Instance = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(World? x, World? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x is null || y is null)
            return false;
        return EqualityComparer<WorldId>.Default.Equals(x.id, y.id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(World obj) => obj.id.GetHashCode();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Compare(World? x, World? y)
    {
        if (ReferenceEquals(x, y))
            return 0;
        if (x is null)
            return -1;
        if (y is null)
            return 1;
        return WorldId.DefaultComparer.Compare(x.id, y.id);
    }
}
    
sealed class WorldIdComparer : IEqualityComparer<WorldId>, IComparer<WorldId>
{
    public static readonly WorldIdComparer Instance = new();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(WorldId x, WorldId y) => x.Equals(y);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(WorldId obj) => obj.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Compare(WorldId x, WorldId y)
    {
        if (x.Equals(y))
            return 0;
        if (x.index1 == y.index1)
            return x.generation - y.generation;
        return x.index1 - y.index1;
    }
}