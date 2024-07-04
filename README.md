# IOGesture
[![NuGet](https://img.shields.io/nuget/dt/IOGesture?logo=nuget)](https://www.nuget.org/packages/IOGesture)
![GitHub License](https://img.shields.io/github/license/iolitetech/IOGesture)
![GitHub Release](https://img.shields.io/github/v/release/iolitetech/IOGesture)


A lightweight and easy-to-use library for handling touch and gesture events in Blazor applications. This wrapper is built on top of the [TinyGesture](https://github.com/sciactive/tinygesture) library and provides seamless integration with Blazor components.

## Features
- **Pan Gestures**: Detect and handle pan gestures with start, move, and end callbacks.
- **Swipe Gestures**: Easily recognize swipe gestures in all directions (up, down, left, right).
- **Tap Gestures**: Identify single tap and double tap gestures.
- **Long Press**: Handle long press events.
- **Pinch and Rotate**: Manage pinch (zoom) and rotate gestures, complete with start and end callbacks.
- **Customizable Options**: Configure gesture thresholds and other settings to fine-tune the gesture detection.

:information_source: For more information please check the [TinyGesture](https://github.com/sciactive/tinygesture) repository.
## Getting Started
### Installation
1. Install [IOGesture](https://www.nuget.org/packages/IOGesture) with dotnet cli in your Blazor app.

  ```sh
  dotnet add package IOGesture 
  ```

2. Add the tinygesture.js and IOGesture.js script tag in your root file **_Host.cshtml for Blazor Server Apps** or **index.html for Blazor WebAssembly Apps**:
  ```html

  <script src="_framework/blazor.server.js"></script>
  <script src="_content/IOGesture/js/IOGesture.js" type="module"></script>
  <script src="_content/IOGesture/js/tinygesture.js" type="module"></script>
  ```
## Usage
Use the `IOGesture.Components` and use the component: 
```Razor
@using IOGesture.Components

<IOGesture>
  <h1>Swipe me.</h1>
</IOGesture>
```
Then you can listen to gesture event callbacks and pass the options with type of `GestureOptions`, You can access various properties of the gesture using the `Properties` object after you referenced the `IOGesture` instance:
```Razor
<IOGesture 
    @ref="ioGesture"
    OnPanMove="HandlePanMove"
    Options="gestureOptions">
    <Content>
        <!-- Your content here -->
    </Content>
</IOGesture>

@code {
    private IOGesture? ioGesture;
    private GestureOptions gestureOptions = new GestureOptions
    {
        VelocityThreshold = 10,
        PressThreshold = 8,
        DiagonalSwipes = false,
        DiagonalLimit = 15,
        MouseSupport = true
    };

    private void HandlePanMove()
    {
        Console.WriteLine($"X: {_panMovingInstance.Properties.TouchMoveX}, Y: {_panMovingInstance.Properties.TouchMoveY}");
    }

    private void HandleSwipeLeft()
    {
        // Handle swipe left
    }
}
```
### GestureOptions Configuration
Customize gesture detection by setting properties in GestureOptions:

- **`VelocityThreshold`**: Minimum velocity the gesture must be moving when the gesture ends to be considered a swipe.
- **`PressThreshold`**: Point at which the pointer moved too much to consider it a tap or long press gesture.
- **`DiagonalSwipes`**: If true, swiping in a diagonal direction will fire both a horizontal and a vertical swipe.
- **`DiagonalLimit`**: The degree limit to consider a diagonal swipe when DiagonalSwipes is true.
- **`MouseSupport`**: Listen to mouse events in addition to touch events (for desktop support).

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/iolitetech/IOGesture/blob/master/LICENSE.txt) file for details.

## Acknowledgements
This wrapper is built on top of the [TinyGesture](https://github.com/sciactive/tinygesture) library.
