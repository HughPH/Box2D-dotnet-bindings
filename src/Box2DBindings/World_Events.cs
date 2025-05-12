namespace Box2D;

public partial class World
{
    /// <summary>
    /// Event handler for body move events
    /// </summary>
    public delegate void BodyMoveEventHandler(in BodyMoveEvent @event);

    /// <summary>
    /// The body move event handler. This is called when a body moves.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all valid BodyMoveEvents.<br/>
    /// <i>Note: This is not called for all bodies, only for those that are valid. The number of events may be different to those found in the world's BodyEvents.</i>
    /// </remarks>
    public BodyMoveEventHandler? BodyMove;

    private unsafe void BodyMoveTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var bodyMoveEvent = ((BodyMoveEvent*)events)[i];
            if (bodyMoveEvent.Body.Valid)
                BodyMove?.Invoke(bodyMoveEvent);
        }
    }
    
    /// <summary>
    /// Event handler for sensor begin touch events
    /// </summary>
    public delegate void SensorBeginTouchEventHandler(in SensorBeginTouchEvent @event);

    /// <summary>
    /// The sensor begin touch event handler. This is called when a sensor begins touching another shape.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all SensorBeginTouchEvents.<br/>
    /// <i>Note: This is not called for all sensor shapes, only for those that are valid. The number of events may be different to those found in the world's SensorEvents.</i>
    /// </remarks>
    public SensorBeginTouchEventHandler? SensorBeginTouch;

    private unsafe void SensorBeginTouchTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var sensorBeginTouchEvent = ((SensorBeginTouchEvent*)events)[i];
            if (sensorBeginTouchEvent.SensorShape.Valid && sensorBeginTouchEvent.VisitorShape.Valid)
                SensorBeginTouch?.Invoke(sensorBeginTouchEvent);
        }
    }
    
    /// <summary>
    /// Event handler for sensor end touch events
    /// </summary>
    public delegate void SensorEndTouchEventHandler(in SensorEndTouchEvent @event);

    /// <summary>
    /// The sensor end touch event handler. This is called when a sensor ends touching another shape.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all SensorEndTouchEvents.<br/>
    /// <i>Note: This is not called for all sensor shapes, only for those that are valid. The number of events may be different to those found in the world's SensorEvents.</i>
    /// </remarks>
    public SensorEndTouchEventHandler? SensorEndTouch;

    private unsafe void SensorEndTouchTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var sensorEndTouchEvent = ((SensorEndTouchEvent*)events)[i];
            if (sensorEndTouchEvent.SensorShape.Valid && sensorEndTouchEvent.VisitorShape.Valid)
                SensorEndTouch?.Invoke(sensorEndTouchEvent);
        }
    }
    
    /// <summary>
    /// Event handler for contact begin touch events
    /// </summary>
    public delegate void ContactBeginTouchEventHandler(in ContactBeginTouchEvent @event);

    /// <summary>
    /// The contact begin touch event handler. This is called when a contact begins touching another shape.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all ContactBeginTouchEvents.<br/>
    /// <i>Note: This is not called for all contact shapes, only for those that are valid. The number of events may be different to those found in the world's ContactEvents.</i>
    /// </remarks>
    public ContactBeginTouchEventHandler? ContactBeginTouch;

    private unsafe void ContactBeginTouchTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var contactBeginTouchEvent = ((ContactBeginTouchEvent*)events)[i];
            if (contactBeginTouchEvent.ShapeA.Valid && contactBeginTouchEvent.ShapeB.Valid)
                ContactBeginTouch?.Invoke(contactBeginTouchEvent);
        }
    }
    
    /// <summary>
    /// Event handler for contact end touch events
    /// </summary>
    public delegate void ContactEndTouchEventHandler(in ContactEndTouchEvent @event);

    /// <summary>
    /// The contact end touch event handler. This is called when a contact ends touching another shape.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all ContactEndTouchEvents.<br/>
    /// <i>Note: This is not called for all contact shapes, only for those that are valid. The number of events may be different to those found in the world's ContactEvents.</i>
    /// </remarks>
    public ContactEndTouchEventHandler? ContactEndTouch;
    
    private unsafe void ContactEndTouchTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var contactEndTouchEvent = ((ContactEndTouchEvent*)events)[i];
            if (contactEndTouchEvent.ShapeA.Valid && contactEndTouchEvent.ShapeB.Valid)
                ContactEndTouch?.Invoke(contactEndTouchEvent);
        }
    }
    
    /// <summary>
    /// Event handler for contact hit events
    /// </summary>
    public delegate void ContactHitEventHandler(in ContactHitEvent @event);
    
    /// <summary>
    /// The contact hit event handler. This is called when a contact hits another shape.
    /// </summary>
    /// <remarks>If this is set, the World will raise events for all ContactHitEvents.<br/>
    /// <i>Note: This is not called for all contact shapes, only for those that are valid. The number of events may be different to those found in the world's ContactEvents.</i>
    /// </remarks>
    public ContactHitEventHandler? ContactHit;
    
    private unsafe void ContactHitTaskCallback(int startIndex, int endIndex, uint workerIndex, nint events)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            var contactHitEvent = ((ContactHitEvent*)events)[i];
            if (contactHitEvent.ShapeA.Valid && contactHitEvent.ShapeB.Valid)
                ContactHit?.Invoke(contactHitEvent);
        }
    }
}