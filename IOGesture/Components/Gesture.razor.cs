using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using IOGesture.Models;

namespace IOGesture.Components
{
    
    public partial class Gesture : IAsyncDisposable
    {
        private ElementReference _container;

        private DotNetObjectReference<Gesture>? _dotNetHelper = null;
        private const string InteropNameSpace = "ioGesture";
        
        [Inject] 
        public IJSRuntime _JsRuntime { get; set; }

        /// <summary>
        /// CSS classes to be applied to the component.
        /// </summary>
        [Parameter]
        public string? Classes { get; set; } = null;

        /// <summary>
        /// CSS styles to be applied to the component.
        /// </summary>
        [Parameter]
        public string? Styles { get; set; } = null;
        /// <summary>
        /// Gets or sets the id as it will be used in the HTML.
        /// </summary>
        [Parameter] 
        public string? Id { get; set; } = null;
        /// <summary>
        /// Labels to be applied to the component.
        /// </summary>
        [Parameter] 
        public string? Labels { get; set; } = null;
        /// <summary>
        /// Content to be rendered inside the component.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        
        /// <summary>
        /// HTML attributes to be applied to the component.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> Attributes { get; set; }
        /// <summary>
        /// Options for configuring gesture behavior.
        /// </summary>
        [Parameter]
        public GestureOptions? Options { get; set; }
        /// <summary>
        /// Event callback for when a pan gesture starts.
        /// </summary>
        [Parameter]
        public EventCallback OnPanStart { get; set; }
        
        /// <summary>
        /// Event callback for when a pan gesture moves.
        /// </summary>
        [Parameter]
        public EventCallback OnPanMove { get; set; }
        
        /// <summary>
        /// Event callback for when a pan gesture ends.
        /// </summary>
        [Parameter]
        public EventCallback OnPanEnd { get; set; }
        
        /// <summary>
        /// Event callback for when a swipe to the right is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnSwipeRight { get; set; }
        
        /// <summary>
        /// Event callback for when a swipe to the left is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnSwipeLeft { get; set; }
        
        
        /// <summary>
        /// Event callback for when a swipe to the up is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnSwipeUp { get; set; }
        
        
        /// <summary>
        /// Event callback for when a swipe to the down is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnSwipeDown { get; set; }
        /// <summary>
        /// Event callback for when a single tap gesture is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnTap { get; set; }
        
        /// <summary>
        /// Event callback for when a long press gesture is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnLongPress { get; set; }
        
        /// <summary>
        /// Event callback for when a double tap gesture is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnDoubleTap { get; set; }
        
        /// <summary>
        /// Event callback for when a pinch gesture is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnPinch { get; set; }
        
        /// <summary>
        /// Event callback for when a pinch gesture ends.
        /// </summary>
        [Parameter]
        public EventCallback OnPinchEnd { get; set; }
        
        /// <summary>
        /// Event callback for when a rotate gesture is detected.
        /// </summary>
        [Parameter]
        public EventCallback OnRotate { get; set; }
        
        /// <summary>
        /// Event callback for when a rotate gesture ends.
        /// </summary>
        [Parameter]
        public EventCallback OnRotateEnd { get; set; }

        private int InstanceId { get; set; }
        public GestureProperties Properties { get; internal set; } = new();
        

        public Gesture()
        {
            _dotNetHelper = DotNetObjectReference.Create(this);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Options is null)
                    Options = new GestureOptions();
                if (_container.Context is not null)
                {
                    InstanceId = await Create();
                }
            }
        }

        internal async ValueTask<int> Create()
        {
           return await _JsRuntime.InvokeAsync<int>($"{InteropNameSpace}.create", _container, Options, _dotNetHelper);
        }

        [JSInvokable]
        public async Task OnPanStartCallback(double touchStartX, double touchStartY)
        {
            Properties.TouchStartX = touchStartX;
            Properties.TouchStartY = touchStartY;
            
            if (OnPanStart.HasDelegate)
            {
                await OnPanStart.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnPanMoveCallback(PanMoveModel panMoveModel)
        {

            Properties.TouchStartX = panMoveModel.TouchStartX;
            Properties.TouchStartY = panMoveModel.TouchStartY;
            Properties.TouchMoveX = panMoveModel.TouchMoveX;
            Properties.TouchMoveY = panMoveModel.TouchMoveY;
            Properties.IsSwipingHorizontal = panMoveModel.IsSwipingHorizontal;
            Properties.IsSwipingVertical = panMoveModel.IsSwipingVertical;
            Properties.SwipingDirection = panMoveModel.SwipingDirection;
            Properties.VelocityX = panMoveModel.VelocityX;
            Properties.VelocityY = panMoveModel.TouchMoveY;
            Properties.Direction = GetDirection(panMoveModel.SwipingDirection, panMoveModel.TouchMoveX,
                panMoveModel.TouchMoveY);
                
            if (OnPanMove.HasDelegate)
            {
                await OnPanMove.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnPanEndCallback(PanEndModel panEndModel)
        {
            Properties.TouchStartX = panEndModel.TouchStartX;
            Properties.TouchStartY = panEndModel.TouchStartY;
            Properties.TouchMoveX = panEndModel.TouchMoveX;
            Properties.TouchMoveY = panEndModel.TouchMoveY;
            Properties.IsSwipingHorizontal = panEndModel.IsSwipingHorizontal;
            Properties.IsSwipingVertical = panEndModel.IsSwipingVertical;
            Properties.SwipingDirection = panEndModel.SwipingDirection;
            Properties.VelocityX = panEndModel.VelocityX;
            Properties.VelocityY = panEndModel.TouchMoveY;
            Properties.TouchEndX = panEndModel.TouchEndX;
            Properties.TouchEndY = panEndModel.TouchEndY;
            Properties.Direction = GetDirection(panEndModel.SwipingDirection, panEndModel.TouchMoveX,
                panEndModel.TouchMoveY);
            
            if (OnPanEnd.HasDelegate)
            {
                await OnPanEnd.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnSwipeRightCallback(bool isSwipedHorizontal, bool isSwipedVertical)
        {
            Properties.IsSwipingHorizontal = isSwipedHorizontal;
            Properties.IsSwipingVertical = isSwipedVertical;
            
            if (OnSwipeRight.HasDelegate)
            {
                await OnSwipeRight.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnSwipeLeftCallback(bool isSwipedHorizontal, bool isSwipedVertical)
        {
            Properties.IsSwipingHorizontal = isSwipedHorizontal;
            Properties.IsSwipingVertical = isSwipedVertical;
            
            if (OnSwipeRight.HasDelegate)
            {
                await OnSwipeLeft.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnSwipeUpCallback(bool isSwipedHorizontal, bool isSwipedVertical)
        {
            Properties.IsSwipingHorizontal = isSwipedHorizontal;
            Properties.IsSwipingVertical = isSwipedVertical;
            
            if (OnSwipeRight.HasDelegate)
            {
                await OnSwipeUp.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnSwipeDownCallback(bool isSwipedHorizontal, bool isSwipedVertical)
        {
            Properties.IsSwipingHorizontal = isSwipedHorizontal;
            Properties.IsSwipingVertical = isSwipedVertical;
            
            if (OnSwipeRight.HasDelegate)
            {
                await OnSwipeDown.InvokeAsync();
            }
        }

        [JSInvokable]
        public async Task OnTapCallback()
        {
            if (OnTap.HasDelegate)
            {
                await OnTap.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnLongPressCallback()
        {
            if (OnLongPress.HasDelegate)
            {
                await OnLongPress.InvokeAsync();
            }
            
        }
        
        [JSInvokable]
        public async Task OnDoubleTapCallback()
        {
            if (OnDoubleTap.HasDelegate)
            {
                await OnDoubleTap.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnPinchCallback(double scale)
        {
            Properties.Scale = scale;
            
            if (OnPinch.HasDelegate)
            {
                await OnPinch.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnPinchEndCallback()
        {
            if (OnPinchEnd.HasDelegate)
            {
                await OnPinchEnd.InvokeAsync();
            }
        }
        
        [JSInvokable]
        public async Task OnRotateCallback(double angle)
        {
            Properties.RotationAngle = angle;
            
            if (OnRotate.HasDelegate)
            {
                await OnRotate.InvokeAsync();
            }
        }

        [JSInvokable]
        public async Task OnRotateEndCallback()
        {
            if (OnRotateEnd.HasDelegate)
            {
                await OnRotateEnd.InvokeAsync();
            }
        }
        
        private SwipeDirection GetDirection(string swipeDirection, double? touchMoveX, double? touchMoveY) 
        {
            switch (swipeDirection)
            {
                case "horizontal":
                    if (touchMoveX < 0)
                        return SwipeDirection.Left;
                    else
                         return SwipeDirection.Right;
                
                case "vertical":
                    if (touchMoveY < 0)
                        return SwipeDirection.Up;
                    else
                        return SwipeDirection.Down;
                    
                default:
                    return SwipeDirection.None;
                   
            }
        }
        public async ValueTask DisposeAsync()
        {
            await _JsRuntime.InvokeVoidAsync("ioGesture.disposeInstance", InstanceId);
            if (_dotNetHelper is not null)
                _dotNetHelper.Dispose();
        }
    }
}