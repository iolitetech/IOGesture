namespace IOGesture.Components;

public class GestureProperties
{
    /// <summary>
    /// The (screen) x coordinate of the start of the gesture.
    /// </summary>
    public double? TouchStartX { get; set; }
    
    /// <summary>
    /// The (screen) y coordinate of the start of the gesture.
    /// </summary>
    public double? TouchStartY { get; set; }
    /// <summary>
    ///The amount the gesture has moved in the x direction.
    /// </summary>
    public double? TouchMoveX { get; set; }
    
    /// <summary>
    ///The amount the gesture has moved in the y direction.
    /// </summary>
    public double? TouchMoveY { get; set; }
    /// <summary>
    /// The instantaneous velocity in the x direction.
    /// </summary>
    public double? VelocityX { get; set; }
    
    /// <summary>
    /// The instantaneous velocity in the y direction.
    /// </summary>
    public double? VelocityY { get; set; }
    /// <summary>
    /// whether the gesture has passed the swiping threshold in the x direction.
    /// </summary>
    public bool? IsSwipingHorizontal { get; set; }
    
    /// <summary>
    /// whether the gesture has passed the swiping threshold in the y direction.
    /// </summary>
    public bool? IsSwipingVertical { get; set; }
    /// <summary>
    /// Which direction the gesture has moved most. Prefixed with 'pre-' if the
    /// gesture hasn't passed the corresponding threshold.
    /// One of: ['horizontal', 'vertical', 'pre-horizontal', 'pre-vertical']
    /// </summary>
    public string? SwipingDirection { get; set; }
    /// <summary>
    /// The (screen) x coordinate of the end of the gesture.
    /// </summary>
    public double? TouchEndX { get; set; }
    
    /// <summary>
    /// The (screen) y coordinate of the end of the gesture.
    /// </summary>
    public double? TouchEndY { get; set; }
    /// <summary>
    /// The current scale of the pinch
    /// less than 1 means the user is zooming out.
    /// greater than 1 means the user is zooming in.
    /// </summary>
    public double? Scale { get; set; }
    /// <summary>
    /// The current angle of the rotation, in degrees.
    /// </summary>
    public double? RotationAngle { get; set; }
    
    /// <summary>
    /// The swiping direction.
    /// </summary>
    public SwipeDirection Direction { get; set; }
}