using Box2D;
using System.Numerics;
using Vec2 = System.Numerics.Vector2;

namespace UnitTests;

[Collection("Sequential")]
public class PolygonDecomposeTests
{
    [Fact]
    public void Decompose_ConvexPolygon_SplitsWhenTooManyVertices()
    {
        int n = 10; // > MAX_POLYGON_VERTICES
        Vec2[] pts = new Vec2[n];
        float step = 2 * MathF.PI / n;
        for (int i = 0; i < n; ++i)
            pts[i] = new Vec2(MathF.Cos(step * i), MathF.Sin(step * i));

        Polygon[] parts = Polygon.Decompose(pts);
        Assert.True(parts.Length > 1);
        foreach (var p in parts)
            Assert.True(p.Vertices.Length <= Constants.MAX_POLYGON_VERTICES);
    }

    [Fact]
    public void Decompose_ConcavePolygon_ProducesValidPieces()
    {
        Vec2[] pts =
        {
            new(-2f,-2f), new(2f,-2f), new(2f,-1f), new(1f,-1f), new(1f,1f),
            new(2f,1f), new(2f,2f), new(-2f,2f), new(-2f,-1f), new(-1f,-1f)
        };

        Polygon[] parts = Polygon.Decompose(pts);
        Assert.NotEmpty(parts);
        foreach (var p in parts)
            Assert.True(p.Vertices.Length <= Constants.MAX_POLYGON_VERTICES);
    }
}
