using JetBrains.Annotations;
using System;

namespace Box2D;

/// <summary>
/// Gear calculation functionality no included in Box2D.
/// </summary>
[PublicAPI]
public class Gears
{
    /// <summary>
    /// Create the shapes that represent an approximate <a href="https://en.wikipedia.org/wiki/Involute_gear">Involute Gear</a> on the supplied Body, situated at the Body's origin.
    /// </summary>
    /// <param name="body">The Body to attach the shapes to.</param>
    /// <param name="shapeDef">The ShapeDef to use for the shapes.</param>
    /// <param name="pitchRadius">The pitch radius of the gear - that is, where the teeth engage.</param>
    /// <param name="teeth">The number of teeth that the gear should have.</param>
    /// <param name="toothLength">The length of the teeth - how far they stick out from the sprocket.</param>
    /// <returns>A list of shapes that were added to the Body.</returns>
    /// <exception cref="ArgumentOutOfRangeException">>If teeth is less than 3</exception>
    public ReadOnlySpan<Shape> CreateInvoluteGear(
        Body body, ShapeDef shapeDef,
        float pitchRadius, int teeth, float toothLength)
    {
        if (teeth < 3)
            throw new ArgumentOutOfRangeException(nameof(teeth), "Must have at least 3 teeth");

        var gear = new Shape[teeth + 1];

        float addendum = toothLength * 0.6f; // tip above pitch circle
        float dedendum = toothLength * 0.4f; // root below pitch circle

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
    
    /// <summary>
    /// Calculate the closest possible pitch radius, number of teeth and resulting gear ratio for a new gear based on the current gear pitch radius, number of teeth, and a gear ratio.
    /// </summary>
    /// <param name="pitchRadius">The pitch radius of the first gear.</param>
    /// <param name="teeth">The number of teeth on the first gear.</param>
    /// <param name="ratio">The desired gear ratio. A ratio of 1 means the second gear is the same size as the first gear. A value below 1 means the second gear is smaller, and a value above 1 means the second gear is larger.</param>
    /// <returns>The pitch radius, number of teeth and actual gear ratio for the new gear.</returns>
    /// <remarks>This method is intended to help find a size of gear that will mesh with the first gear, adjusting the ratio if necessary.</remarks>
    public (float pitchRadius, int teeth, float ratio) GetNearestGearMetrics(float pitchRadius, int teeth, float ratio)
    {
        int newTeeth = (int)Math.Round(teeth * ratio, 0);
        if (newTeeth < 3)
            newTeeth = 3;
        float newPitchRadius = pitchRadius * newTeeth / teeth;
        float newRatio = (float)newTeeth / teeth;
        return (newPitchRadius, newTeeth, newRatio);
    }
    
    /// <summary>
    /// Calculate compatible gear metrics for a second gear with matching circular pitch,
    /// based on a desired gear ratio and known tooth length of the first gear.
    /// This version clamps the tooth length of the second gear to ensure meshing clearance.
    /// </summary>
    /// <param name="pitchRadius">The pitch radius of the first gear.</param>
    /// <param name="teeth">The number of teeth on the first gear.</param>
    /// <param name="toothLength">tooth length of the first gear.</param>
    /// <param name="ratio">Desired gear ratio (second gear : first gear).</param>
    /// <returns>
    /// The second gear's pitch radius, number of teeth, safe tooth length, and the distance between the gear centres.
    /// </returns>
    /// <remarks>This method is intended to help find a size of gear and tooth length that will mesh with the first gear, without adjusting the ratio.</remarks>
    public (float pitchRadius, int teeth, float toothLength, float centreDistance) GetGearMetrics(
        float pitchRadius, int teeth, float toothLength, float ratio)
    {
        if (teeth < 3)
            throw new ArgumentOutOfRangeException(nameof(teeth), "First gear must have at least 3 teeth");
        if (toothLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(toothLength), "Tooth length must be > 0");
        if (ratio <= 0)
            throw new ArgumentOutOfRangeException(nameof(ratio), "Gear ratio must be > 0");

        // Preserve ratio exactly
        float newPitchRadius = pitchRadius * ratio;
        int newTeeth = Math.Max(3, (int)(teeth * ratio)); // still needs to be an int

        // Estimate first gear's addendum based on 60/40 split
        float addendumA = toothLength * 0.6f;
        float outerRadiusA = pitchRadius + addendumA;

        float centreDistance = pitchRadius + newPitchRadius;

        // Outer radius of gear B must not exceed the remaining space
        float maxOuterRadiusB = centreDistance - outerRadiusA;
        float maxAddendumB = MathF.Max(0f, maxOuterRadiusB - newPitchRadius);
        float safeToothLengthB = maxAddendumB / 0.6f;

        return (newPitchRadius, newTeeth, safeToothLengthB, centreDistance);
    }

    
}
