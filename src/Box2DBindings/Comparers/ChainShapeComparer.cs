using JetBrains.Annotations;
using System.Collections.Generic;

namespace Box2D.Comparers
{
    [PublicAPI]
    sealed class ChainShapeComparer : IEqualityComparer<Box2D.ChainShape>, IComparer<Box2D.ChainShape>
    {
        public static readonly ChainShapeComparer Instance = new();

        public bool Equals(Box2D.ChainShape x, Box2D.ChainShape y) => x.Equals(y);

        public int GetHashCode(Box2D.ChainShape obj) => obj.GetHashCode();
        
        public int Compare(Box2D.ChainShape x, Box2D.ChainShape y) => x.Equals(y) ? 0 : x.GetHashCode() - y.GetHashCode();
    }
}
