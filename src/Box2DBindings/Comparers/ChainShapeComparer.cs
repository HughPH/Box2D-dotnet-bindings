using JetBrains.Annotations;
using System.Collections.Generic;

namespace Box2D.Comparers
{
    [PublicAPI]
    sealed class ChainShapeComparer : IEqualityComparer<ChainShape>, IComparer<ChainShape>
    {
        public static readonly ChainShapeComparer Instance = new();

        public bool Equals(ChainShape x, ChainShape y) => x.Equals(y);

        public int GetHashCode(ChainShape obj) => obj.GetHashCode();
        
        public int Compare(ChainShape x, ChainShape y) => x.Equals(y) ? 0 : Comparer<ChainShape>.Default.Compare(x, y);
    }
}
