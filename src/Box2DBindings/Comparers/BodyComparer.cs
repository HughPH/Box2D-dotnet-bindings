using JetBrains.Annotations;
using System.Collections.Generic;

namespace Box2D.Comparers;

[PublicAPI]
sealed class BodyComparer : IEqualityComparer<Body>, IComparer<Body>
{
    public static readonly BodyComparer Instance = new();

    public bool Equals(Body x, Body y) => x.Equals(y);

    public int GetHashCode(Body obj) => obj.GetHashCode();
        
    public int Compare(Body x, Body y) => x.Equals(y) ? 0 : x.GetHashCode() - y.GetHashCode();
}