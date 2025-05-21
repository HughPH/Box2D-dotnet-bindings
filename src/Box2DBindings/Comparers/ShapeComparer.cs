using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Box2D.Comparers;

[PublicAPI]
sealed class ShapeComparer : IEqualityComparer<Shape>, IComparer<Shape>
{
    public static readonly ShapeComparer Instance = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Shape x, Shape y) => x.Equals(y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(Shape obj) => obj.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Compare(Shape x, Shape y) => x.Equals(y) ? 0 : Comparer<Shape>.Default.Compare(x, y);
}