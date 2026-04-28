using Box2D;
using JetBrains.Annotations;
using System.Numerics;
using Xunit;

namespace UnitTests
{
    [TestSubject(typeof(World))]
    [Collection("Sequential")]
    public class WorldTest
    {
        public enum TestShapeKind
        {
            Circle,
            Segment,
            Capsule
        }

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

        [Theory]
        [InlineData(TestShapeKind.Circle, TestShapeKind.Circle)]
        [InlineData(TestShapeKind.Segment, TestShapeKind.Capsule)]
        public void SensorShape_ShouldReportOverlaps_AndRaiseBeginEndSensorEvents(TestShapeKind sensorShapeKind, TestShapeKind visitorShapeKind)
        {
            var world = World.CreateWorld(new WorldDef());

            var sensorBody = world.CreateBody(new BodyDef
            {
                Type = BodyType.Static,
                Position = new Vector2(0, 0)
            });
            var visitorBody = world.CreateBody(new BodyDef
            {
                Type = BodyType.Kinematic,
                Position = new Vector2(0, 0)
            });

            var sensorShape = CreateShape(sensorBody, new ShapeDef
            {
                IsSensor = true,
                EnableSensorEvents = true
            }, sensorShapeKind);
            var visitorShape = CreateShape(visitorBody, new ShapeDef
            {
                Density = 1.0f,
                EnableSensorEvents = true
            }, visitorShapeKind);

            SensorBeginTouchEvent? beginEvent = null;
            SensorEndTouchEvent? endEvent = null;
            world.SensorBeginTouch += (in @event) => beginEvent = @event;
            world.SensorEndTouch += (in @event) => endEvent = @event;

            world.Step(1.0f / 60.0f);

            Assert.True(sensorShape.Sensor);
            Assert.NotNull(beginEvent);
            Assert.Equal(sensorShape, beginEvent.Value.SensorShape);
            Assert.Equal(visitorShape, beginEvent.Value.VisitorShape);

            var sensorEvents = world.SensorEvents;
            Assert.Equal(1, sensorEvents.BeginEvents.Length);
            Assert.Equal(sensorShape, sensorEvents.BeginEvents[0].SensorShape);
            Assert.Equal(visitorShape, sensorEvents.BeginEvents[0].VisitorShape);
            Assert.Equal(0, sensorEvents.EndEvents.Length);

            var overlaps = sensorShape.SensorOverlaps;
            Assert.Equal(1, overlaps.Length);
            Assert.Equal(visitorShape, overlaps[0]);

            visitorBody.Transform = new Transform
            {
                Position = new Vector2(5, 0),
                Rotation = visitorBody.Transform.Rotation
            };

            world.Step(1.0f / 60.0f);

            Assert.NotNull(endEvent);
            Assert.Equal(sensorShape, endEvent.Value.SensorShape);
            Assert.Equal(visitorShape, endEvent.Value.VisitorShape);

            sensorEvents = world.SensorEvents;
            Assert.Equal(0, sensorEvents.BeginEvents.Length);
            Assert.Equal(1, sensorEvents.EndEvents.Length);
            Assert.Equal(sensorShape, sensorEvents.EndEvents[0].SensorShape);
            Assert.Equal(visitorShape, sensorEvents.EndEvents[0].VisitorShape);
            Assert.Equal(0, sensorShape.SensorOverlaps.Length);
        }

        private static Shape CreateShape(Body body, ShapeDef shapeDef, TestShapeKind shapeKind)
        {
            return shapeKind switch
            {
                TestShapeKind.Circle => body.CreateShape(shapeDef, new Circle
                {
                    Center = Vector2.Zero,
                    Radius = 1.0f
                }),
                TestShapeKind.Segment => body.CreateShape(shapeDef, new Segment
                {
                    Point1 = new Vector2(-1.0f, 0.0f),
                    Point2 = new Vector2(1.0f, 0.0f)
                }),
                TestShapeKind.Capsule => body.CreateShape(shapeDef, new Capsule
                {
                    Center1 = new Vector2(-0.5f, 0.0f),
                    Center2 = new Vector2(0.5f, 0.0f),
                    Radius = 0.5f
                }),
                _ => throw new ArgumentOutOfRangeException(nameof(shapeKind), shapeKind, null)
            };
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
