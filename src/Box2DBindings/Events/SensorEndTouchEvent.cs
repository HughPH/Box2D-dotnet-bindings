using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// An end touch event is generated when a shape stops overlapping a sensor shape.
///	These include things like setting the transform, destroying a body or shape, or changing
///	a filter. You will also get an end event if the sensor or visitor are destroyed.
///	Therefore you should always confirm the shape id is valid using Shape.Valid.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SensorEndTouchEvent
{
    /// <summary>
    /// The id of the sensor shape
    ///	<b>Warning: this shape may have been destroyed</b>
    /// </summary>
    public readonly Shape SensorShapeId;

    /// <summary>
    /// The id of the dynamic shape that stopped touching the sensor shape
    ///	<b>Warning: this shape may have been destroyed</b>
    /// </summary>
    public readonly Shape VisitorShapeId;
}