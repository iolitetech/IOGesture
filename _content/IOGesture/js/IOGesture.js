import TinyGesture from "./tinygesture.js";

const gestures = new Map();
let nextId = 0;

export function create(target, options, helper) {
    const instance = new TinyGesture(target, options);
    const id = nextId++;

    const preventDefaultListener = (event) => {
        event.preventDefault();
    };
    target.addEventListener('touchstart', preventDefaultListener, { passive: false });

    addEventListeners(instance, helper);
    gestures.set(id, { instance, preventDefaultListener, target });
    return id;
}

export function disposeInstance(id) {
    const entry = gestures.get(id);
    if (entry) {
        entry.instance.destroy();
        entry.target.removeEventListener('touchstart', entry.preventDefaultListener);
        gestures.delete(id);
    }
}

function addEventListeners(instance, helper) {
    instance.on("panstart", async event => {
        await helper.invokeMethodAsync("OnPanStartCallback", instance.touchStartX, instance.touchStartY);
    });

    instance.on("panmove", async event => {
        const args = {
            touchStartX: instance.touchStartX,
            touchStartY: instance.touchStartY,
            touchMoveX: instance.touchMoveX,
            touchMoveY: instance.touchMoveY,
            velocityX: instance.velocityX,
            velocityY: instance.velocityY,
            isSwipingHorizontal: instance.swipingHorizontal,
            isSwipingVertical: instance.swipingVertical,
            swipingDirection: instance.swipingDirection
        };
        await helper.invokeMethodAsync("OnPanMoveCallback", args);
    });

    instance.on("panend", async event => {
        const args = {
            touchStartX: instance.touchStartX,
            touchStartY: instance.touchStartY,
            touchMoveX: instance.touchMoveX,
            touchMoveY: instance.touchMoveY,
            velocityX: instance.velocityX,
            velocityY: instance.velocityY,
            isSwipingHorizontal: instance.swipingHorizontal,
            isSwipingVertical: instance.swipingVertical,
            swipingDirection: instance.swipingDirection,
            touchEndX: instance.touchEndX,
            touchEndY: instance.touchEndY
        };
        await helper.invokeMethodAsync("OnPanEndCallback", args);
    });

    instance.on("swiperight", async event => {
        await helper.invokeMethodAsync("OnSwipeRightCallback", instance.swipedHorizontal, instance.swipedVertical);
    });

    instance.on("swipeleft", async event => {
        await helper.invokeMethodAsync("OnSwipeLeftCallback", instance.swipedHorizontal, instance.swipedVertical);
    });

    instance.on("swipeup", async event => {
        await helper.invokeMethodAsync("OnSwipeUpCallback", instance.swipedHorizontal, instance.swipedVertical);
    });

    instance.on("swipedown", async event => {
        await helper.invokeMethodAsync("OnSwipeDownCallback", instance.swipedHorizontal, instance.swipedVertical);
    });

    instance.on("tap", async event => {
        await helper.invokeMethodAsync("OnTapCallback");
    });

    instance.on("longpress", async event => {
        await helper.invokeMethodAsync("OnLongPressCallback");
    });

    instance.on("doubletap", async event => {
        await helper.invokeMethodAsync("OnDoubleTapCallback");
    });

    instance.on("pinch", async event => {
        await helper.invokeMethodAsync("OnPinchCallback", instance.scale);
    });

    instance.on("pinchend", async event => {
        await helper.invokeMethodAsync("OnPinchEndCallback");
    });

    instance.on("rotate", async event => {
        await helper.invokeMethodAsync("OnRotateCallback", instance.rotation);
    });

    instance.on("rotateend", async event => {
        await helper.invokeMethodAsync("OnRotateEndCallback");
    });
}
