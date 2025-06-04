using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Box2D.Comparers
{
    [PublicAPI]
    sealed class ChainShapeComparer : IEqualityComparer<ChainShape>, IComparer<ChainShape>
    {
        public static readonly ChainShapeComparer Instance = new();

        public bool Equals(ChainShape x, ChainShape y) => x.Equals(y);

        public int GetHashCode(ChainShape obj) => HashCode.Combine(obj.id.index1, obj.id.world0, obj.id.generation);
        
        public int Compare(ChainShape x, ChainShape y) => x.Equals(y) ? 0 : Comparer<ChainShapeId>.Default.Compare(x.id, y.id);
    }
}
