
namespace IOGesture.Components
{
    public class GestureOptions
    {
        /// <summary>
        /// Minimum velocity the gesture must be moving when the gesture ends to be
        /// considered a swipe.
        /// </summary>
        public int VelocityThreshold { get; set; } = 10;
        /// <summary>
        /// Point at which the pointer moved too much to consider it a tap or longpress gesture.
        /// 
        /// </summary>
        public int PressThreshold { get; set; } = 8;

        /// <summary>
        /// If true, swiping in a diagonal direction will fire both a horizontal and a
        /// vertical swipe.
        /// If false, whichever direction the pointer moved more will be the only swipe
        /// fired.
        /// </summary>
        public bool DiagonalSwipes { get; set; } = false;

        /// <summary>
        /// The degree limit to consider a diagonal swipe when diagonalSwipes is true.
        /// It's calculated as 45deg±diagonalLimit.
        /// </summary>
        public int DiagonalLimit { get; set; } = 15;

        /// <summary>
        /// Listen to mouse events in addition to touch events. (For desktop support.)
        /// </summary>
        public bool MouseSupport { get; set; } = true;

    }
}
