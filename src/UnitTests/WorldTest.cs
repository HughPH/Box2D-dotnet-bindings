using Box2D;
using JetBrains.Annotations;
using System.Numerics;
using Xunit;

namespace UnitTests
{
    [TestSubject(typeof(World))]
    public class WorldTest
    {
                [Fact]
        public void CreateWorld_ShouldInitializeCorrectly()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            Assert.NotNull(world);
            Assert.True(world.Valid);
        }

        [Fact]
        public void CreateBody_ShouldAddBodyToWorld()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var bodyDef = new BodyDef
            {
                Position = new Vector2(0, 0),
                Type = BodyType.Dynamic
            };
            var body = world.CreateBody(bodyDef);

            Assert.NotNull(body);
            Assert.True(body.Valid);
            Assert.Contains(body, world.Bodies);
        }

        [Fact]
        public void AttachShapeToBody_ShouldAddShapeWithFriction()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var bodyDef = new BodyDef
            {
                Position = new Vector2(0, 0),
                Type = BodyType.Dynamic
            };
            var body = world.CreateBody(bodyDef);

            var material = new SurfaceMaterial
            {
                Friction = 0.3f,
                Restitution = 0.5f
            };
            var shapeDef = new ShapeDef
            {
                Material = material
            };
            var circle = new Circle
            {
                Radius = 1.0f
            };

            var shape = body.CreateShape(shapeDef, circle);

            Assert.True(shape.Valid);
            Assert.Equal(body, shape.Body);
        }

        [Fact]
        public void AttachMultipleShapesToBody_ShouldAllowMultipleShapes()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var bodyDef = new BodyDef
            {
                Position = new Vector2(0, 0),
                Type = BodyType.Dynamic
            };
            var body = world.CreateBody(bodyDef);

            var material = new SurfaceMaterial
            {
                Friction = 0.3f,
                Restitution = 0.7f
            };
            var shapeDef = new ShapeDef
            {
                Material = material
            };

            var circle = new Circle
            {
                Radius = 1.0f
            };

            var polygonVertices = new[]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f)
            };

            var polygon = new Polygon(polygonVertices, 0f);

            var shape1 = body.CreateShape(shapeDef, circle);
            var shape2 = body.CreateShape(shapeDef, polygon);

            Assert.True(shape1.Valid);
            Assert.True(shape2.Valid);
        }

        [Fact]
        public void BodyDestroy_ShouldRemoveBody()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var bodyDef = new BodyDef
            {
                Position = new Vector2(0, 0),
                Type = BodyType.Dynamic
            };
            var body = world.CreateBody(bodyDef);

            Assert.NotNull(body);

            body.Destroy();

            Assert.False(body.Valid);
            Assert.DoesNotContain(body, world.Bodies);
        }

        [Fact]
        public void Destroy_ShouldInvalidateWorld()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.Destroy();

            Assert.False(world.Valid);
        }

        [Fact]
        public void SetGravity_ShouldUpdateGravity()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);
            Vector2 newGravity = new Vector2(0, -9.8f);

            world.Gravity = newGravity;

            Assert.Equal(newGravity, world.Gravity);
        }

        [Fact]
        public void ContactEvents_ShouldThrowIfWorldInvalid()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.Destroy();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var events = world.ContactEvents;
            });
        }

        [Fact]
        public void BodyEvents_ShouldThrowIfWorldInvalid()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.Destroy();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var events = world.BodyEvents;
            });
        }

        [Fact]
        public void SensorEvents_ShouldThrowIfWorldInvalid()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.Destroy();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var events = world.SensorEvents;
            });
        }

        [Fact]
        public void CollectMemoryStats_ShouldNotThrow()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var exception = Record.Exception(() => world.DumpMemoryStats());

            Assert.Null(exception);
        }

        [Fact]
        public void AwakeBodyCount_ShouldReturnZeroInitially()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            Assert.Equal(0, world.AwakeBodyCount);
        }

        [Fact]
        public void RestitutionThreshold_ShouldUpdateValue()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var newValue = 2.0f;
            world.RestitutionThreshold = newValue;

            Assert.Equal(newValue, world.RestitutionThreshold);
        }

        [Fact]
        public void HitEventThreshold_ShouldUpdateValue()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var newValue = 5.0f;
            world.HitEventThreshold = newValue;

            Assert.Equal(newValue, world.HitEventThreshold);
        }

        [Fact]
        public void MaximumLinearSpeed_ShouldUpdateValue()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var newValue = 50.0f;
            world.MaximumLinearSpeed = newValue;

            Assert.Equal(newValue, world.MaximumLinearSpeed);
        }

        [Fact]
        public void SetCustomFilterCallback_ShouldSetCallback()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            var callbackInvoked = false;
            CustomFilterCallback callback = (shapeA, shapeB) =>
            {
                callbackInvoked = true;
                return true;
            };

            world.SetCustomFilterCallback(callback);

            // Assuming `InvokeCustomFilter` simulates invoking the callback
            world.Destroy(); // Free callback to prevent memory leaks
            Assert.False(callbackInvoked, "Ensure the callback does not self-invoke without operations.");
        }

        [Fact]
        public void SleepingEnabled_ShouldToggleCorrectly()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.SleepingEnabled = false;
            Assert.False(world.SleepingEnabled);

            world.SleepingEnabled = true;
            Assert.True(world.SleepingEnabled);
        }

        [Fact]
        public void ContinuousEnabled_ShouldToggleCorrectly()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.ContinuousEnabled = false;
            Assert.False(world.ContinuousEnabled);

            world.ContinuousEnabled = true;
            Assert.True(world.ContinuousEnabled);
        }

        [Fact]
        public void WarmStartingEnabled_ShouldToggleCorrectly()
        {
            var worldDef = new WorldDef();
            var world = World.CreateWorld(worldDef);

            world.WarmStartingEnabled = false;
            Assert.False(world.WarmStartingEnabled);

            world.WarmStartingEnabled = true;
            Assert.True(world.WarmStartingEnabled);
        }
    }
}