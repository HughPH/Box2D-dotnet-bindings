using Box2D;
using System.Numerics;
using Xunit;

namespace UnitTests;

public class EventTests
{
    [Fact]
    public void SensorBeginEndEventsRaised()
    {
        int beginCount = 0;
        int endCount = 0;

        World world = World.CreateWorld(new WorldDef());
        world.SensorBeginTouch += _ => beginCount++;
        world.SensorEndTouch += _ => endCount++;

        BodyDef staticDef = new() { Type = BodyType.Static };
        Body sensorBody = world.CreateBody(staticDef);

        ShapeDef sensorShapeDef = new();
        sensorShapeDef.IsSensor = true;
        sensorShapeDef.EnableSensorEvents = true;
        sensorBody.CreateShape(sensorShapeDef, new Circle { Radius = 1f });

        BodyDef dynamicDef = new() { Type = BodyType.Dynamic, Position = new(-2f, 0f) };
        Body dynamicBody = world.CreateBody(dynamicDef);

        ShapeDef dynamicShapeDef = new() { Density = 1f };
        dynamicShapeDef.EnableSensorEvents = true;
        dynamicBody.CreateShape(dynamicShapeDef, new Circle { Radius = 0.5f });

        dynamicBody.LinearVelocity = new Vector2(5f, 0f);
        for (int i = 0; i < 60; ++i)
        {
            world.Step();
        }

        Assert.True(beginCount > 0, "Sensor begin touch event not raised");
        Assert.True(endCount > 0, "Sensor end touch event not raised");
    }

    [Fact]
    public void ContactBeginEndHitEventsRaised()
    {
        int beginCount = 0;
        int endCount = 0;
        int hitCount = 0;

        World world = World.CreateWorld(new WorldDef());
        world.ContactBeginTouch += _ => beginCount++;
        world.ContactEndTouch += _ => endCount++;
        world.ContactHit += _ => hitCount++;

        BodyDef bodyDefA = new() { Type = BodyType.Dynamic, Position = new(-2f, 0f) };
        BodyDef bodyDefB = new() { Type = BodyType.Dynamic, Position = new(2f, 0f) };
        Body bodyA = world.CreateBody(bodyDefA);
        Body bodyB = world.CreateBody(bodyDefB);

        ShapeDef shapeDef = new() { Density = 1f };
        shapeDef.EnableContactEvents = true;
        shapeDef.EnableHitEvents = true;

        Circle circle = new() { Radius = 0.5f };
        bodyA.CreateShape(shapeDef, circle);
        bodyB.CreateShape(shapeDef, circle);

        bodyA.LinearVelocity = new Vector2(5f, 0f);
        bodyB.LinearVelocity = new Vector2(-5f, 0f);
        for (int i = 0; i < 120; ++i)
        {
            world.Step();
        }

        Assert.True(beginCount > 0, "Contact begin touch event not raised");
        Assert.True(hitCount > 0, "Contact hit event not raised");
        Assert.True(endCount > 0, "Contact end touch event not raised");
    }
}
