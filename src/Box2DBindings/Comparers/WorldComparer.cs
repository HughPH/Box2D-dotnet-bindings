using System.Collections.Generic;

namespace Box2D.Comparers
{
    public class WorldComparer : IEqualityComparer<World>, IComparer<World>
    {
        public static readonly WorldComparer Instance = new();
        
        public bool Equals(World x, World y) => x.id.Equals(y.id);
        
        public int GetHashCode(World obj) => obj.GetHashCode();
        
        public int Compare(World x, World y)
        {
            return x.id.Equals(y.id) ? 0 : x.id.GetHashCode() - y.id.GetHashCode();
        }
    }
    
    class WorldIdComparer : IEqualityComparer<WorldId>, IComparer<WorldId>
    {
        public static readonly WorldIdComparer Instance = new();
        
        public bool Equals(WorldId x, WorldId y) => x.Equals(y);
        
        public int GetHashCode(WorldId obj) => obj.GetHashCode();
        
        public int Compare(WorldId x, WorldId y)
        {
            return x.Equals(y) ? 0 : x.GetHashCode() - y.GetHashCode();
        }
    }
}