using Box2D;
using System.Linq;
using Vec2 = System.Numerics.Vector2;

namespace UnitTests;

public class AABBTests
{
    [Fact]
    public void MakeAABB_ComputesCorrectBounds()
    {
        Vec2[] points = { new(-1f, -2f), new(3f, 4f), new(0f, 1f) };
        float radius = 0.5f;
        AABB aabb = AABB.MakeAABB(points, radius);

        Vec2 expectedLower = new(-1f, -2f);
        expectedLower = Vec2.Min(expectedLower, new(0f, 1f));
        Vec2 expectedUpper = new(3f, 4f);
        expectedUpper = Vec2.Max(expectedUpper, new(0f, 1f));
        Vec2 r = new(radius, radius);

        Assert.Equal(expectedLower - r, aabb.LowerBound);
        Assert.Equal(expectedUpper + r, aabb.UpperBound);
    }

    [Fact]
    public void Equality_And_ToString_Work()
    {
        Vec2 lower = new(1f, 2f);
        Vec2 upper = new(3f, 5f);
        AABB aabb1 = new(lower, upper);
        AABB aabb2 = new(lower, upper);

        Assert.Equal(aabb1, aabb2);
        Assert.Equal(aabb1.GetHashCode(), aabb2.GetHashCode());

        string str = aabb1.ToString();
        Assert.Contains("AABB", str);
        Assert.Contains(lower.ToString(), str);
        Assert.Contains(upper.ToString(), str);
    }
}
