using Box2D;
using System.Numerics;
using Xunit.Abstractions;

namespace UnitTests;

[Collection("Sequential")]
public class CreationTests
{
    public CreationTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    private readonly ITestOutputHelper _output;
    
    [Fact]
    public void CreateWorldFromDefault()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef def = WorldDef.Default;
        World world = World.CreateWorld(def);
        if (error is not null) Assert.Fail(error);
    }

    [Fact]
    public void CreateWorldDefFromNew()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef def = new WorldDef();
        WorldDef fromDefault = WorldDef.Default;
        Assert.Equal(def.MaximumLinearSpeed, fromDefault.MaximumLinearSpeed);
        if (error is not null) Assert.Fail(error);
    }

    [Fact]
    public void CreateWorld_WithGravityAssignedAfterDefaultConstructor_AppliesGravity()
    {
        Vector2 gravity = -Vector2.UnitY * 60;
        WorldDef worldDef = new();
        worldDef.Gravity = gravity;
        World world = new(worldDef);
        Body body = CreateDynamicBody(world);

        Assert.Equal(gravity, worldDef.Gravity);
        Assert.Equal(gravity, world.Gravity);
        world.Step(1.0f / 60.0f, 4);
        Assert.True(body.LinearVelocity.Y < 0, $"Expected gravity to move body downward; velocity was {body.LinearVelocity}");
    }

    [Fact]
    public void CreateWorld_WithGravityPassedToConstructor_AppliesGravity()
    {
        Vector2 gravity = -Vector2.UnitY * 60;
        WorldDef worldDef = new(gravity: gravity);
        World world = new(worldDef);
        Body body = CreateDynamicBody(world);

        Assert.Equal(gravity, worldDef.Gravity);
        Assert.Equal(gravity, world.Gravity);
        world.Step(1.0f / 60.0f, 4);
        Assert.True(body.LinearVelocity.Y < 0, $"Expected gravity to move body downward; velocity was {body.LinearVelocity}");
    }

    private static Body CreateDynamicBody(World world)
    {
        BodyDef bodyDef = new()
        {
            Type = BodyType.Dynamic
        };

        Body body = world.CreateBody(bodyDef);
        ShapeDef shapeDef = new()
        {
            Density = 1.0f
        };
        Circle circle = new()
        {
            Radius = 1.0f
        };
        body.CreateShape(shapeDef, circle);

        return body;
    }

    [Fact]
    public void DefConstructors_WithOmittedOptionalParameters_PreserveNativeDefaults()
    {
        World world = new(new WorldDef());
        Body bodyA = world.CreateBody(new BodyDef());
        Body bodyB = world.CreateBody(new BodyDef());

        ShapeDef defaultShapeDef = new();
        ShapeDef shapeDef = new(defaultShapeDef.Material, defaultShapeDef.Density, defaultShapeDef.Filter);
        Assert.Equal(defaultShapeDef.InvokeContactCreation, shapeDef.InvokeContactCreation);

        DistanceJointDef defaultDistanceJointDef = new();
        DistanceJointDef distanceJointDef = new(bodyA, bodyB, default, default);
        Assert.Equal(defaultDistanceJointDef.Length, distanceJointDef.Length);
        Assert.Equal(defaultDistanceJointDef.MaxLength, distanceJointDef.MaxLength);

        MouseJointDef defaultMouseJointDef = new();
        MouseJointDef mouseJointDef = new(bodyA, bodyB, default);
        Assert.Equal(defaultMouseJointDef.Hertz, mouseJointDef.Hertz);
        Assert.Equal(defaultMouseJointDef.DampingRatio, mouseJointDef.DampingRatio);
        Assert.Equal(defaultMouseJointDef.MaxForce, mouseJointDef.MaxForce);

        MotorJointDef defaultMotorJointDef = new();
        MotorJointDef motorJointDef = new(bodyA, bodyB, default, 0.0f);
        Assert.Equal(defaultMotorJointDef.MaxForce, motorJointDef.MaxForce);
        Assert.Equal(defaultMotorJointDef.MaxTorque, motorJointDef.MaxTorque);

        WheelJointDef defaultWheelJointDef = new();
        WheelJointDef wheelJointDef = new(bodyA, bodyB, default, default, default);
        Assert.Equal(defaultWheelJointDef.EnableSpring, wheelJointDef.EnableSpring);
        Assert.Equal(defaultWheelJointDef.Hertz, wheelJointDef.Hertz);
        Assert.Equal(defaultWheelJointDef.DampingRatio, wheelJointDef.DampingRatio);
    }

    [Fact]
    public void DefConstructors_WithExplicitOptionalValues_OverwriteNativeDefaults()
    {
        World world = new(new WorldDef());
        Body bodyA = world.CreateBody(new BodyDef());
        Body bodyB = world.CreateBody(new BodyDef());

        ShapeDef defaultShapeDef = new();
        ShapeDef shapeDef = new(defaultShapeDef.Material, defaultShapeDef.Density, defaultShapeDef.Filter, invokeContactCreation: false);
        Assert.False(shapeDef.InvokeContactCreation);

        DistanceJointDef distanceJointDef = new(bodyA, bodyB, default, default, length: 0.0f, maxLength: 0.0f);
        Assert.Equal(0.0f, distanceJointDef.Length);
        Assert.Equal(0.0f, distanceJointDef.MaxLength);

        MouseJointDef mouseJointDef = new(bodyA, bodyB, default, hertz: 0.0f, dampingRatio: 0.0f, maxForce: 0.0f);
        Assert.Equal(0.0f, mouseJointDef.Hertz);
        Assert.Equal(0.0f, mouseJointDef.DampingRatio);
        Assert.Equal(0.0f, mouseJointDef.MaxForce);

        MotorJointDef motorJointDef = new(bodyA, bodyB, default, 0.0f, maxForce: 0.0f, maxTorque: 0.0f);
        Assert.Equal(0.0f, motorJointDef.MaxForce);
        Assert.Equal(0.0f, motorJointDef.MaxTorque);

        WheelJointDef wheelJointDef = new(bodyA, bodyB, default, default, default, enableSpring: false, hertz: 0.0f, dampingRatio: 0.0f);
        Assert.False(wheelJointDef.EnableSpring);
        Assert.Equal(0.0f, wheelJointDef.Hertz);
        Assert.Equal(0.0f, wheelJointDef.DampingRatio);
    }

    [Fact]
    void CreateTwoJointedBodies()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef worldDf = new WorldDef();
        World world = World.CreateWorld(worldDf);

        BodyDef bodyDef = new BodyDef();
        bodyDef.Type = BodyType.Dynamic;
        bodyDef.Position = new(-10f, 0f);
        Body bodyA = world.CreateBody(bodyDef);

        bodyDef.Position = new(10f, 0f);
        Body bodyB = world.CreateBody(bodyDef);

        DistanceJointDef jointDef = new DistanceJointDef();
        jointDef.BodyA = bodyA;
        jointDef.BodyB = bodyB;
        jointDef.LocalAnchorA = new(0f, 0f);
        jointDef.LocalAnchorB = new(0f, 0f);
        
        Joint joint = world.CreateJoint(jointDef);
        
        if (error is not null) Assert.Fail(error);
    }
    
    [Fact]
    void CreateChainShape()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef worldDf = new WorldDef();
        World world = World.CreateWorld(worldDf);

        BodyDef bodyDef = new BodyDef();
        bodyDef.Type = BodyType.Static;
        bodyDef.Position = new(0f, 0f);
        Body bodyA = world.CreateBody(bodyDef);

        Vector2[] vertices =
        {
            new(-5f, -10),
            new(-3.2f, 10),
            new(-3.2f, 0),
            new(3.2f, 0),
            new(3.2f, 10),
            new(5f, -10),
            new(-5f, -10)
        };

        ChainDef chainDef = new ChainDef()
            {
                Points = vertices,
                IsLoop = true
            };
        
        ChainShape chainShape = bodyA.CreateChain(chainDef);

        // Materials is set by Box2D, and so it has a pointer that we didn't create.
        // We should have a check in the Materials property and the finalizer to
        // make sure the one we're trying to Free is our own. If this fails, then
        // that would be the first place to look.
        chainDef.Materials = [];
        
        if (error is not null) Assert.Fail(error);
    }
    
    [Fact]
    void GetBodyMassData()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef worldDf = new WorldDef();
        World world = World.CreateWorld(worldDf);

        BodyDef bodyDef = new BodyDef();
        bodyDef.Type = BodyType.Dynamic;
        bodyDef.Position = new(-10f, 0f);
        Body bodyA = world.CreateBody(bodyDef);

        ShapeDef shapeDef = new ShapeDef();
        shapeDef.Material = new SurfaceMaterial
            {
                Friction = 0.5f,
                Restitution = 0.5f,
                RollingResistance = 0.5f,
                TangentSpeed = 0.5f
            };
        shapeDef.Density = 1f;
        shapeDef.Filter.CategoryBits = 0x0004;
        shapeDef.Filter.MaskBits = 0x0004;
        shapeDef.Filter.GroupIndex = 0;
        
        Circle circle = new Circle();
        circle.Radius = 1f;
        circle.Center = new(0f, 0f);
        
        bodyA.CreateShape(shapeDef, circle);
        
        MassData massData = bodyA.MassData;
        
        Assert.Equal(MathF.PI, massData.Mass);
        
        if (error is not null) Assert.Fail(error);
    }
    
    [Fact]
    void BodyEventTest()
    {
        string? error = null;
        Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        WorldDef worldDf = new WorldDef(){EnableParallelEvents = true};
        World world = World.CreateWorld(worldDf);

        world.BodyMove += OnWorldBodyMove;
        
        BodyDef bodyDef = new BodyDef();
        bodyDef.Type = BodyType.Dynamic;
        bodyDef.Position = new(-10f, 0f);
        Body bodyA = world.CreateBody(bodyDef);

        ShapeDef shapeDef = new ShapeDef();
        shapeDef.Material = new SurfaceMaterial
            {
                Friction = 0.5f,
                Restitution = 0.5f,
                RollingResistance = 0.5f,
                TangentSpeed = 0.5f
            };
        shapeDef.Density = 1f;
        shapeDef.Filter.CategoryBits = 0x0004;
        shapeDef.Filter.MaskBits = 0x0004;
        shapeDef.Filter.GroupIndex = 0;
        
        Circle circle = new Circle();
        circle.Radius = 1f;
        circle.Center = new(0f, 0f);
        
        bodyA.CreateShape(shapeDef, circle);
        
        Body bodyB = world.CreateBody(bodyDef);
        Transform transform = bodyB.Transform;
        transform.Position = new(10f, 0f);
        bodyB.Transform = transform;
        bodyB.CreateShape(shapeDef, circle);
        
        world.Step(0.1f,4);
    }
    
    private void OnWorldBodyMove(in BodyMoveEvent args)
    {
        Assert.False(args.FellAsleep);
        _output.WriteLine($"Body {args.Body} moved to {args.Transform.Position}");
    }
}
