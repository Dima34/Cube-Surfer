using UnityEngine;
using Cinemachine;

namespace Infrastructure.Logic
{
    [AddComponentMenu("")]
    public class CameraLockExtension : CinemachineExtension
    {
        [SerializeField] private bool _lockX;
        [SerializeField] private bool _lockY;
        [SerializeField] private float _lockCameraXPositionValue;
        [SerializeField] private float _lockCameraYPositionValue;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (enabled && stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;

                if (_lockX)
                    pos.x = _lockCameraXPositionValue;
                
                if(_lockY)
                    pos.y = _lockCameraYPositionValue;
                
                state.RawPosition = pos;
            }
        }
    }
}