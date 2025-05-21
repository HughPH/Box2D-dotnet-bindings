using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Box2D.Comparers
{
    [PublicAPI]
    sealed class ChainShapeComparer : IEqualityComparer<ChainShape>, IComparer<ChainShape>
    {
        public static readonly ChainShapeComparer Instance = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ChainShape x, ChainShape y) => x.Equals(y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(ChainShape obj) => obj.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ChainShape x, ChainShape y) => x.Equals(y) ? 0 : Comparer<ChainShape>.Default.Compare(x, y);
    }
}
