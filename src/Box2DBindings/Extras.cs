using System;

namespace Box2D;

/// <summary>
/// Extra functionality no included in Box2D.
/// </summary>
public class Extras
{
    /// <summary>
    /// Create the shapes that represent a Gear on the supplied Body, situated at the Body's origin.
    /// </summary>
    /// <param name="body">The Body to attach the shapes to.</param>
    /// <param name="shapeDef">The ShapeDef to use for the shapes.</param>
    /// <param name="radius">The pitch radius of the gear, excluding teeth.</param>
    /// <param name="teeth">The number of teeth that the gear should have.</param>
    /// <param name="toothWidth">The width a tooth where it meets the sprocket.</param>
    /// <param name="toothLength">The length of the teeth - how far they stick out from the sprocket.</param>
    /// <returns>A list of shapes that were added to the Body.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If teeth is less than 3</exception>
    /// <exception cref="ArgumentOutOfRangeException">If toothWidth is less than or equal to 0</exception>
    /// <exception cref="ArgumentOutOfRangeException">If toothHeight is less than or equal to 0</exception>
    public ReadOnlySpan<Shape> CreateSimpleGear(Body body, ShapeDef shapeDef, float radius, int teeth, float toothWidth, float toothLength)
    {
        if (teeth < 3)
            throw new ArgumentOutOfRangeException(nameof(teeth), "Must have at least 3 teeth");
        if (toothWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(toothWidth), "Tooth width must be greater than 0");
        if (toothLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(toothLength), "Tooth height must be greater than 0");

        var gear = new Shape[teeth + 1];
        Circle circle = new Circle(Vec2.Zero, radius);
        gear[0] = new Shape(body, shapeDef, circle);

        float anglePerTooth = 2 * MathF.PI / teeth;
        float baseRadius = radius;
        float tipRadius = radius + toothLength;

        float baseHalfWidthAngle = toothWidth / (2 * radius); // approx arc angle width
        float tipHalfWidthAngle = baseHalfWidthAngle * 0.5f; // narrower tip (50% of base width)

        for (int i = 0; i < teeth; i++)
        {
            float angle = i * anglePerTooth;

            float angleA = angle - baseHalfWidthAngle;
            float angleB = angle + baseHalfWidthAngle;
            float angleC = angle + tipHalfWidthAngle;
            float angleD = angle - tipHalfWidthAngle;

            Vec2 bl = new Vec2(MathF.Cos(angleA) * baseRadius, MathF.Sin(angleA) * baseRadius); // bottom left
            Vec2 br = new Vec2(MathF.Cos(angleB) * baseRadius, MathF.Sin(angleB) * baseRadius); // bottom right
            Vec2 tr = new Vec2(MathF.Cos(angleC) * tipRadius, MathF.Sin(angleC) * tipRadius); // top right
            Vec2 tl = new Vec2(MathF.Cos(angleD) * tipRadius, MathF.Sin(angleD) * tipRadius); // top left

            Polygon polygon = new Polygon(new[] { tl, tr, br, bl }); // clockwise
            gear[i + 1] = new Shape(body, shapeDef, polygon);
        }

        return gear;
    }

    /// <summary>
    /// Create the shapes that represent an approximage <a href="https://en.wikipedia.org/wiki/Involute_gear">Involute Gear</a> on the supplied Body, situated at the Body's origin.
    /// </summary>
    /// <param name="body">The Body to attach the shapes to.</param>
    /// <param name="shapeDef">The ShapeDef to use for the shapes.</param>
    /// <param name="pitchRadius">The pitch radius of the gear, excluding teeth.</param>
    /// <param name="teeth">The number of teeth that the gear should have.</param>
    /// <param name="toothLength">The length of the teeth - how far they stick out from the sprocket.</param>
    /// <returns>A list of shapes that were added to the Body.</returns>
    /// <exception cref="ArgumentOutOfRangeException">>If teeth is less than 3</exception>
    public ReadOnlySpan<Shape> CreateGear(
        Body body, ShapeDef shapeDef,
        float pitchRadius, int teeth, float toothHeight)
    {
        if (teeth < 3)
            throw new ArgumentOutOfRangeException(nameof(teeth), "Must have at least 3 teeth");

        var gear = new Shape[teeth + 1];

        float addendum = toothHeight * 0.6f; // tip above pitch circle
        float dedendum = toothHeight * 0.4f; // root below pitch circle

        float rootRadius = pitchRadius - dedendum;
        float outerRadius = pitchRadius + addendum;

        gear[0] = new Shape(body, shapeDef, new Circle(Vec2.Zero, rootRadius));

        float anglePerTooth = 2 * MathF.PI / teeth;
        float halfToothAngle = anglePerTooth * 0.5f;

        // Angle offsets for 3 flank segments (per side)
        float flank1 = halfToothAngle * 0.33f;
        float flank2 = halfToothAngle * 0.66f;

        // Local helper
        Vec2 FromAngle(float angle) => new(MathF.Cos(angle), MathF.Sin(angle));

        for (int i = 0; i < teeth; i++)
        {
            float centerAngle = i * anglePerTooth;

            // Root
            Vec2 rootLeft = FromAngle(centerAngle - halfToothAngle) * rootRadius;
            Vec2 rootRight = FromAngle(centerAngle + halfToothAngle) * rootRadius;

            // Inner flank
            Vec2 innerFlankLeft = FromAngle(centerAngle - flank1) * (pitchRadius - addendum * 0.5f);
            Vec2 innerFlankRight = FromAngle(centerAngle + flank1) * (pitchRadius - addendum * 0.5f);

            // Outer flank
            Vec2 outerFlankLeft = FromAngle(centerAngle - flank2) * (pitchRadius + addendum * 0.25f);
            Vec2 outerFlankRight = FromAngle(centerAngle + flank2) * (pitchRadius + addendum * 0.25f);

            // Tip
            Vec2 tipLeft = FromAngle(centerAngle - flank2 * 0.85f) * outerRadius;
            Vec2 tipRight = FromAngle(centerAngle + flank2 * 0.85f) * outerRadius;

            // Clockwise order
            var points = new[]
                {
                    rootLeft,
                    innerFlankLeft,
                    outerFlankLeft,
                    tipLeft,
                    tipRight,
                    outerFlankRight,
                    innerFlankRight,
                    rootRight
                };

            gear[i + 1] = new Shape(body, shapeDef, new Polygon(points));
        }

        return gear;
    }
}
