using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Box2D.Comparers;

[PublicAPI]
sealed class BodyComparer : IEqualityComparer<Body>, IComparer<Body>
{
    public static readonly BodyComparer Instance = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Body x, Body y) => x.Equals(y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(Body obj) => obj.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Compare(Body x, Body y) => x.Equals(y) ? 0 : Comparer<Body>.Default.Compare(x, y);
}