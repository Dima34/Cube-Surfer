namespace Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        protected override float GetXAxis() =>
            GetTouchOnScreenPercent();

        protected override bool IsStartTap() =>
            UnityEngine.Input.touchCount > 0;
    }
}