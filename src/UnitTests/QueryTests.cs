using Box2D;
using Vec2 = System.Numerics.Vector2;
using Xunit;

namespace UnitTests;

public class QueryTests
{
    [Fact]
    public void CastRayClosest_HitsDynamicShape()
    {
        var world = World.CreateWorld(new WorldDef());
        var bodyDef = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
        var body = world.CreateBody(bodyDef);
        var shapeDef = new ShapeDef { Density = 1f };
        var circle = new Circle { Radius = 1f };
        var shape = body.CreateShape(shapeDef, circle);

        RayResult result = world.CastRayClosest(new Vec2(-5f, 0f), new Vec2(10f, 0f), new QueryFilter());

        Assert.True(result.Hit, "RayCast should hit the dynamic shape");
        Assert.Equal(shape, result.Shape);
        Assert.InRange(result.Fraction, 0f, 1f);
    }

    [Fact]
    public void CastRay_CallbackInvoked()
    {
        var world = World.CreateWorld(new WorldDef());
        var bodyDef = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
        var body = world.CreateBody(bodyDef);
        var shapeDef = new ShapeDef { Density = 1f };
        body.CreateShape(shapeDef, new Circle { Radius = 0.5f });

        int hits = 0;
        world.CastRay(new Vec2(-2f, 0f), new Vec2(4f, 0f), new QueryFilter(), (s, p, n, f) =>
        {
            hits++;
            return 0f; // terminate after first hit
        });

        Assert.Equal(1, hits);
    }

    [Fact]
    public void OverlapAABB_FindsShape()
    {
        var world = World.CreateWorld(new WorldDef());
        var bodyDef = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(1, 1) };
        var body = world.CreateBody(bodyDef);
        var shapeDef = new ShapeDef { Density = 1f };
        var circle = body.CreateShape(shapeDef, new Circle { Radius = 0.5f });

        int count = 0;
        OverlapResultCallback callback = s =>
        {
            count++;
            return true;
        };

        world.OverlapAABB(new AABB(new Vec2(0, 0), new Vec2(2, 2)), new QueryFilter(), ref callback);

        Assert.Equal(1, count);
    }

    [Fact]
    public void CastShape_DetectsCollision()
    {
        var world = World.CreateWorld(new WorldDef());
        var staticBodyDef = new BodyDef { Type = BodyType.Static, Position = new Vec2(2f, 0f) };
        var staticBody = world.CreateBody(staticBodyDef);
        var shapeDef = new ShapeDef();
        staticBody.CreateShape(shapeDef, new Circle { Radius = 0.5f });

        ShapeProxy proxy = Core.MakeProxy(new Circle { Radius = 0.5f });

        int hits = 0;
        world.CastShape(proxy, new Vec2(3f, 0f), new QueryFilter(), (s, p, n, f) =>
        {
            hits++;
            return 0f;
        });

        Assert.True(hits > 0, "CastShape should detect the static body");
    }
}
