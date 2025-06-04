using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Box2D.Comparers;

[PublicAPI]
sealed class BodyComparer : IEqualityComparer<Body>, IComparer<Body>
{
    public static readonly BodyComparer Instance = new();

    public bool Equals(Body x, Body y) => x.Equals(y);

    public int GetHashCode(Body obj) => HashCode.Combine(obj.index1, obj.world0, obj.generation);
        
    public int Compare(Body x, Body y) => x.Equals(y) ? 0 : Comparer<Body>.Default.Compare(x, y);
}