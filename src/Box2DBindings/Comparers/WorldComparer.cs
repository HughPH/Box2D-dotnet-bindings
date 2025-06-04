using System;
using System.Collections.Generic;

namespace Box2D.Comparers;

sealed class WorldComparer : IEqualityComparer<World>, IComparer<World>
{
    public static readonly WorldComparer Instance = new();

    public bool Equals(World? x, World? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x is null || y is null)
            return false;
        return EqualityComparer<WorldId>.Default.Equals(x.id, y.id);
    }
        
    public int GetHashCode(World obj) => HashCode.Combine(obj.id.index1, obj.id.generation);
        
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
        
    public bool Equals(WorldId x, WorldId y) => x.Equals(y);
        
    public int GetHashCode(WorldId obj) => obj.GetHashCode();

    public int Compare(WorldId x, WorldId y)
    {
        if (x.Equals(y))
            return 0;
        if (x.index1 == y.index1)
            return x.generation - y.generation;
        return x.index1 - y.index1;
    }
}