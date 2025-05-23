using Box2D;
using Xunit;
using System.Numerics;
using Vec2 = System.Numerics.Vector2;

namespace UnitTests;

[Collection("Sequential")]
public class PolygonDecomposeTests
{
    [Fact]
    public void Decompose_Decagon_SplitsIntoPieces()
    {
        Span<Vec2> vertices = stackalloc Vec2[10];
        for (int i = 0; i < 10; ++i)
        {
            float angle = 2 * MathF.PI * i / 10f;
            vertices[i] = new(MathF.Cos(angle), MathF.Sin(angle));
        }

        Polygon[] parts = Polygon.Decompose(vertices);

        Assert.True(parts.Length > 1);
        foreach (var p in parts)
            Assert.InRange(p.Vertices.Length, 3, Constants.MAX_POLYGON_VERTICES);
    }

    [Fact]
    public void Decompose_ConcaveU_Splits()
    {
        Vec2[] vertices =
        [
            new(-4f,-2f),
            new(-4f, 2f),
            new(-2f, 2f),
            new(-2f,-0.5f),
            new( 2f,-0.5f),
            new( 2f, 2f),
            new( 4f, 2f),
            new( 4f,-2f)
        ];

        Polygon[] parts = Polygon.Decompose(vertices);

        Assert.True(parts.Length > 1);
        foreach (var p in parts)
            Assert.InRange(p.Vertices.Length, 3, Constants.MAX_POLYGON_VERTICES);
    }
}
