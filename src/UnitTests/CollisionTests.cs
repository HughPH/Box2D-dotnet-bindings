using Box2D;
using System.Diagnostics;
using Vec2 = System.Numerics.Vector2;

namespace UnitTests
{
    [Collection("Sequential")]
    public class CollisionTests
    {
        private World CreateWorld()
        {
            var worldDef = new WorldDef
                {
                    Gravity = new Vec2(0, -9.8f)
                };
            return World.CreateWorld(worldDef);
        }

        [Fact]
        public void CircleHitEventTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 2) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1, EnableHitEvents = true};

            var circle1 = new Circle { Radius = 1 };
            var circle2 = new Circle { Radius = 1 };

            body1.CreateShape(shapeDef, circle1);
            body2.CreateShape(shapeDef, circle2);
            
            body1.LinearVelocity = new Vec2(0, 1);
            body2.LinearVelocity = new Vec2(0, -1);

            // Act: Simulate for 60 steps (1 second)
            bool collided = false;

            world.ContactHit = (in e) => collided = true;

            int i = 0;
            while (!collided && i++<60)
                world.Step();

            // Assert
            Assert.True(collided, "The two circles should have collided during the simulation.");
        }
        
        [Fact]
        public void CircleTouchEventTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 2) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1, EnableContactEvents = true};

            var circle1 = new Circle { Radius = 1 };
            var circle2 = new Circle { Radius = 1 };

            body1.CreateShape(shapeDef, circle1);
            body2.CreateShape(shapeDef, circle2);
            
            body1.LinearVelocity = new Vec2(0, 1);
            body2.LinearVelocity = new Vec2(0, -1);

            // Act: Simulate for 60 steps (1 second)
            bool collided = false;

            world.ContactBeginTouch = (in e) => collided = true;

            int i = 0;
            while (!collided && i++<60)
                world.Step();

            // Assert
            Assert.True(collided, "The two circles should have collided during the simulation.");
        }

        [Fact]
        public void CapsuleTouchTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 1) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1, EnableContactEvents = true};

            var capsule1 = new Capsule
                {
                    Center1 = new Vec2(0, 0),
                    Center2 = new Vec2(0, 1),
                    Radius = 0.5f
                };

            var capsule2 = new Capsule
                {
                    Center1 = new Vec2(0, 0),
                    Center2 = new Vec2(0, 2),
                    Radius = 0.5f
                };

            body1.CreateShape(shapeDef, capsule1);
            body2.CreateShape(shapeDef, capsule2);

            body1.LinearVelocity = new Vec2(0, -0.5f);

            float totalRadius = capsule1.Radius + capsule2.Radius;

            // Act: Simulate for 2 seconds
            bool collided = false;

            world.ContactBeginTouch = (in e) => collided = true;

            int i = 0;
            while(!collided && i++<120)
                world.Step();

            // Assert
            Assert.True(collided, "The two capsules should have collided during the simulation.");
        }

        [Fact]
        public void PolygonHitTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1, EnableHitEvents = true};

            // Define two polygons
            var polygon1 = new Polygon(new[]
                {
                    new Vec2(-1, -1),
                    new Vec2(1, -1),
                    new Vec2(0, 1)
                });

            var polygon2 = new Polygon(new[]
                {
                    new Vec2(-2, -2),
                    new Vec2(2, -2),
                    new Vec2(0, 2)
                });

            body1.CreateShape(shapeDef, polygon1);
            body2.CreateShape(shapeDef, polygon2);

            body1.LinearVelocity = new Vec2(0, -1); // Move dynamic body downward

            // Variables for collision check
            bool collided = false;

            world.ContactHit = (in e) => collided = true;
            
            // Act: Simulate 1 second
            int i = 0;
            while(!collided && i++<60)
                world.Step();

            // Assert
            Assert.True(collided, "The polygons should have collided during the simulation.");
        }
        
        [Fact]
        public void PolygonTouchTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1, EnableContactEvents = true};

            // Define two polygons
            var polygon1 = new Polygon(new[]
                {
                    new Vec2(-1, -1),
                    new Vec2(1, -1),
                    new Vec2(0, 1)
                });

            var polygon2 = new Polygon(new[]
                {
                    new Vec2(-2, -2),
                    new Vec2(2, -2),
                    new Vec2(0, 2)
                });

            body1.CreateShape(shapeDef, polygon1);
            body2.CreateShape(shapeDef, polygon2);

            body1.LinearVelocity = new Vec2(0, -1); // Move dynamic body downward

            // Variables for collision check
            bool collided = false;

            world.ContactBeginTouch = (in e) => collided = true;
            
            // Act: Simulate 1 second
            int i = 0;
            while(!collided && i++<60)
                world.Step();

            // Assert
            Assert.True(collided, "The polygons should have collided during the simulation.");
        }
    }
}
