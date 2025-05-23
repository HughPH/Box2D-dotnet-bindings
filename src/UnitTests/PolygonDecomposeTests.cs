using Box2D;
using Vec2 = System.Numerics.Vector2;
using Xunit;

namespace UnitTests;

[Collection("Sequential")]
public class PolygonDecomposeTests
{
    [Fact]
    public void DecomposeConvexDecagon()
    {
        Vec2[] poly = new Vec2[10];
        for (int i = 0; i < 10; ++i)
        {
            float angle = i * (2 * System.MathF.PI / 10f);
            poly[i] = new Vec2(System.MathF.Cos(angle), System.MathF.Sin(angle));
        }

        Polygon[] parts = Polygon.Decompose(poly);
        Assert.True(parts.Length > 1);
        foreach (var p in parts)
            Assert.True(p.Vertices.Length <= Constants.MAX_POLYGON_VERTICES);
    }

    [Fact]
    public void DecomposeConcaveUShape()
    {
        Vec2[] poly =
        {
            new(-1f, 0f),
            new(3f, 0f),
            new(3f, 3f),
            new(2f, 3f),
            new(2f, 1f),
            new(1f, 1f),
            new(1f, 3f),
            new(0f, 3f),
            new(0f, 0f)
        };

        Polygon[] parts = Polygon.Decompose(poly);
        Assert.True(parts.Length > 1);
        foreach (var p in parts)
            Assert.True(p.Vertices.Length <= Constants.MAX_POLYGON_VERTICES);
    }
}

