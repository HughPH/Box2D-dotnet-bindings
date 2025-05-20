using Box2D;
using System.Numerics;
using Xunit;

namespace UnitTests;

public class QueryTests
{
    [Fact]
    public void CastRayClosestHitsCircle()
    {
        World world = World.CreateWorld(new WorldDef());
        BodyDef bodyDef = new() { Type = BodyType.Static, Position = new Vector2(5f, 0f) };
        Body body = world.CreateBody(bodyDef);
        body.CreateShape(new ShapeDef(), new Circle { Radius = 1f });

        RayResult result = world.CastRayClosest(Vector2.Zero, new Vector2(10f, 0f), new QueryFilter());

        Assert.True(result.Hit);
        Assert.Equal(body.Shapes[0], result.Shape);
    }

    [Fact]
    public void CastRayInvokesCallback()
    {
        World world = World.CreateWorld(new WorldDef());
        BodyDef bodyDef = new() { Type = BodyType.Static, Position = new Vector2(2f, 0f) };
        Body body = world.CreateBody(bodyDef);
        body.CreateShape(new ShapeDef(), new Circle { Radius = 0.5f });

        int callbackCount = 0;
        world.CastRay(Vector2.Zero, new Vector2(5f, 0f), new QueryFilter(), (in RayResult _) =>
        {
            callbackCount++;
            return true;
        });

        Assert.Equal(1, callbackCount);
    }

    [Fact]
    public void OverlapAABBReturnsShape()
    {
        World world = World.CreateWorld(new WorldDef());
        Body body = world.CreateBody(new BodyDef { Type = BodyType.Static });
        body.CreateShape(new ShapeDef(), new Circle { Radius = 1f });

        int count = 0;
        AABB query = new(new Vector2(-2f, -2f), new Vector2(2f, 2f));
        world.OverlapAABB(query, new QueryFilter(), (Shape _) =>
        {
            count++;
            return true;
        });

        Assert.Equal(1, count);
    }
}
