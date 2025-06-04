using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Box2D.Comparers;

[PublicAPI]
sealed class ShapeComparer : IEqualityComparer<Shape>, IComparer<Shape>
{
    public static readonly ShapeComparer Instance = new();

    public bool Equals(Shape x, Shape y) => x.Equals(y);

    public int GetHashCode(Shape obj) => HashCode.Combine(obj.index1, obj.world0, obj.generation);
        
    public int Compare(Shape x, Shape y) => x.Equals(y) ? 0 : Comparer<Shape>.Default.Compare(x, y);
}