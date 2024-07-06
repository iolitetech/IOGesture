import TinyGesture from "./tinygesture.js";

const gestures = []

window.ioGesture = {

    create: function (target, options, helper) {
        var instance = new TinyGesture(target, options); // Use global TinyGesture
        let foundEmptySlot = false;

        const preventDefaultListener = (event) => {
            event.preventDefault();
        };
        target.addEventListener('touchstart', preventDefaultListener, { passive: false });
        
        for (let i = 0; i < gestures.length; i++) {
            if (!gestures[i]) {
                gestures[i] = instance;
                foundEmptySlot = true;
                this.addEventListeners(i, helper);
                return i;
            }
        }

        // If no empty slot was found, push a new instance
        if (!foundEmptySlot) {
            gestures.push(instance);
            this.addEventListeners(gestures.length - 1, helper);
            return gestures.length - 1;
        }
    },
    addEventListeners: function (index, helper) {
        var gestureObj = gestures[index];
        if (gestureObj) {
            const instance = gestureObj;

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
                await helper.invokeMethodAsync("OnSwipeRightCallback", instance.swipingHorizontal, instance.swipedVertical);
            });

            instance.on("swipeleft", async event => {
                await helper.invokeMethodAsync("OnSwipeLeftCallback", instance.swipingHorizontal, instance.swipedVertical);
            });

            instance.on("swipeup", async event => {
                await helper.invokeMethodAsync("OnSwipeUpCallback", instance.swipingHorizontal, instance.swipedVertical);
            });

            instance.on("swipedown", async event => {
                await helper.invokeMethodAsync("OnSwipeDownCallback", instance.swipingHorizontal, instance.swipedVertical);
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
    },
    disposeInstance: function (index) {
        const instance = gestures[index];
        if (instance) {
            instance.destroy();
            gestures[index] = null;
        }
    }
}
