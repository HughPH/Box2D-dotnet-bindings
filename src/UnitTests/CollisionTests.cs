using Box2D;
using Vec2 = System.Numerics.Vector2;

namespace UnitTests
{
    public class CollisionTests
    {
        private const float TimeStep = 1f / 60f; // 60 FPS
        private World CreateWorld()
        {
            var worldDef = new WorldDef
            {
                Gravity = new Vec2(0, -9.8f)
            };
            return World.CreateWorld(worldDef);
        }

        [Fact]
        public void CircleCollisionTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 2) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1 };

            var circle1 = new Circle { Radius = 1 };
            var circle2 = new Circle { Radius = 1 };

            body1.CreateShape(shapeDef, circle1);
            body2.CreateShape(shapeDef, circle2);

            body1.LinearVelocity = new Vec2(0, 1);
            body2.LinearVelocity = new Vec2(0, -1);

            float totalRadius = circle1.Radius + circle2.Radius;

            // Act: Simulate for 60 steps (1 second)
            bool collided = false;

            for (int i = 0; i < 60; i++) 
            {
                world.Step(TimeStep);

                // Measure the distance between the two bodies
                var distance = Vec2.Distance(body1.Position, body2.Position);

                // Check if a collision occurred
                if (distance <= totalRadius)
                {
                    collided = true;
                    break;
                }
            }

            // Assert
            Assert.True(collided, "The two circles should have collided during the simulation.");
        }

        [Fact]
        public void CapsuleCollisionTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 1) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1 };

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

            for (int i = 0; i < 120; i++) // Simulate 120 steps (2 seconds)
            {
                world.Step(TimeStep);

                // Compute the minimum distance between the two capsules
                float minDistance = MinDistanceBetweenLineSegments(
                    capsule1.Center1 + body1.Position, capsule1.Center2 + body1.Position,
                    capsule2.Center1 + body2.Position, capsule2.Center2 + body2.Position
                );

                // Check if collision occurred
                if (minDistance <= totalRadius)
                {
                    collided = true;
                    break;
                }
            }

            // Assert
            Assert.True(collided, "The two capsules should have collided during the simulation.");
        }

        /// <summary>
        /// Calculates the minimum distance between two line segments.
        /// </summary>
        private float MinDistanceBetweenLineSegments(Vec2 p1, Vec2 q1, Vec2 p2, Vec2 q2)
        {
            // Implementation of minimum distance calculation between two line segments
            return MathF.Sqrt(SquaredDistanceBetweenLineSegments(p1, q1, p2, q2));
        }

        /// <summary>
        /// Calculates the squared minimum distance between two line segments for efficiency.
        /// </summary>
        private float SquaredDistanceBetweenLineSegments(Vec2 p1, Vec2 q1, Vec2 p2, Vec2 q2)
        {
            // Parameters for line segments
            Vec2 d1 = q1 - p1; // Direction vector of segment 1
            Vec2 d2 = q2 - p2; // Direction vector of segment 2
            Vec2 r = p1 - p2;

            float a = Vec2.Dot(d1, d1); // Squared length of segment 1
            float e = Vec2.Dot(d2, d2); // Squared length of segment 2
            float f = Vec2.Dot(d2, r);

            float s, t;

            // Check if either segment is degenerate (zero length)
            if (a <= float.Epsilon && e <= float.Epsilon)
            {
                // Both segments are points
                return Vec2.Dot(r, r);
            }

            if (a <= float.Epsilon)
            {
                // First segment is a point
                s = 0;
                t = Math.Clamp(f / e, 0, 1);
            }
            else
            {
                float c = Vec2.Dot(d1, r);

                if (e <= float.Epsilon)
                {
                    // Second segment is a point
                    t = 0;
                    s = Math.Clamp(-c / a, 0, 1);
                }
                else
                {
                    // General non-degenerate case
                    float b = Vec2.Dot(d1, d2);
                    float denom = a * e - b * b;

                    // If segments are not parallel, compute closest point on segment 1 to segment 2
                    if (denom != 0)
                    {
                        s = Math.Clamp((b * f - c * e) / denom, 0, 1);
                    }
                    else
                    {
                        s = 0; // Parallel case
                    }

                    // Compute point on segment 2 closest to segment 1
                    t = Math.Clamp((b * s + f) / e, 0, 1);
                }
            }

            // Compute closest points
            Vec2 c1 = p1 + s * d1;
            Vec2 c2 = p2 + t * d2;

            return Vec2.Dot(c1 - c2, c1 - c2); // Squared distance
        }

        [Fact]
        public void PolygonCollisionTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1 };

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

            // Act: Simulate 3 seconds
            for (int i = 0; i < 180; i++) // 180 steps = 3 seconds (at 60 FPS)
            {
                world.Step(TimeStep);

                // Check for collision using ShapeDistance
                var input = new DistanceInput
                {
                    TransformA = Transform.Identity, // Identity transform (no rotation or translation)
                    TransformB = Transform.Identity, // Same for second shape
                    ProxyA = Core.MakeProxy(polygon1, 0.0f), // Proxy for polygon1
                    ProxyB = Core.MakeProxy(polygon2, 0.0f), // Proxy for polygon2
                    UseRadii = false
                };

                var cache = new SimplexCache(); // Cache for storing results of GJK/EPA algorithms
                cache.Count = 0;

                var output = Core.ShapeDistance(input, ref cache, new Simplex[3]);
                if (output.Distance <= 0) // Distance <= 0 indicates a collision
                {
                    collided = true;
                    break;
                }
            }

            // Assert
            Assert.True(collided, "The polygons should have collided during the simulation.");
        }

        [Fact]
        public void SegmentCollisionTest()
        {
            // Arrange
            var world = CreateWorld();

            var bodyDef1 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 1) };
            var bodyDef2 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            var shapeDef = new ShapeDef { Density = 1 };

            var segment1 = new Segment { Point1 = new Vec2(-1, 0), Point2 = new Vec2(1, 0) };
            var segment2 = new Segment { Point1 = new Vec2(-2, 0), Point2 = new Vec2(2, 0) };

            body1.CreateShape(shapeDef, segment1);
            body2.CreateShape(shapeDef, segment2);

            body1.LinearVelocity = new Vec2(0, -1); // Move segment1 downward

            bool collided = false;

            // Act: Simulate for 2 seconds
            for (int i = 0; i < 120; i++) // 120 steps = 2 seconds at 60 FPS
            {
                world.Step(TimeStep);

                // Use Core.SegmentDistance to calculate the distance between the two segments
                SegmentDistanceResult distanceResult = Core.SegmentDistance(
                    segment1.Point1 + body1.Position,
                    segment1.Point2 + body1.Position,
                    segment2.Point1 + body2.Position,
                    segment2.Point2 + body2.Position
                );

                // Collision occurs if the closest distance is zero or very small
                if (distanceResult.DistanceSquared <= 0.001f)
                {
                    collided = true;
                    break;
                }
            }

            // Assert
            Assert.True(collided, "The segments should have collided during the simulation.");
        }

        [Fact]
        public void ChainCollisionTest()
        {
            // Arrange
            var world = CreateWorld();

            // Define two bodies
            var bodyDef1 = new BodyDef { Type = BodyType.Static, Position = new Vec2(0, 0) };
            var bodyDef2 = new BodyDef { Type = BodyType.Dynamic, Position = new Vec2(0, 5) };

            var body1 = world.CreateBody(bodyDef1);
            var body2 = world.CreateBody(bodyDef2);

            // Create a chain on body 1 using ChainShape
            var chainShape = new ChainShape(body1, new ChainDef(
                new[]
                    {
                        new Vec2(-3, 1), // Start point on the left
                        new Vec2(0, 0),  // Middle point
                        new Vec2(3, 1)   // End point on the right
                    }
                ));

            // Add a circle shape to body 2
            var circle = new Circle { Radius = 0.5f };

            body2.CreateShape(new ShapeDef { Density = 1 }, circle);

            // Set initial velocity for body 2 (falling downward)
            body2.LinearVelocity = new Vec2(0, -10);

            // Act: Simulate the world for 2 seconds
            for (int i = 0; i < 120; i++) // 120 steps == 2 seconds at 60 FPS
            {
                world.Step(TimeStep);
            }

            // Assert: Check position of body 2 to ensure collision
            var body2Position = body2.Position;

            // The topmost point on the chain is at Y=1, so body2 should not fall below Y=1
            Assert.True(
                body2Position.Y <= 1,
                $"Body2 did not collide properly with the chain. Final Y position: {body2Position.Y}"
                );
        }
    }
}