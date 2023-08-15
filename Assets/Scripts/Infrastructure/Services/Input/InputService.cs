using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public float XAxis => GetXAxis();

        public bool OnStartTap => IsStartTap();

        protected abstract float GetXAxis();
        protected abstract bool IsStartTap();

        protected static Vector2 GetTouchDeltaPosition()
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;
            return touchDeltaPosition;
        }

        internal static float GetTouchOnScreenPercent()
        {
            Vector2 touchDeltaPosition = GetTouchDeltaPosition();
            return touchDeltaPosition.x / Screen.width * 100;
        }
    }
}