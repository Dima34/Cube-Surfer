using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const KeyCode SPACE_KEYCODE = KeyCode.Space;

        protected override float GetXAxis()
        {
            if (IsTouching())
                return GetTouchOnScreenPercent();

            if (UnityEngine.Input.GetAxis(HORIZONTAL_AXIS) is float horizontalAxis)
                return horizontalAxis;

            return 0;
        }

        protected override bool IsStartTap() =>
            IsScreenTouched() || IsSpacebarDown();

        private bool IsSpacebarDown() =>
            UnityEngine.Input.GetKey(SPACE_KEYCODE);

        private static bool IsScreenTouched() =>
            UnityEngine.Input.touchCount > 0;

        private static bool IsTouching() =>
            UnityEngine.Input.touchCount > 0;
    }
}