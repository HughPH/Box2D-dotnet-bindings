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
