using Cinemachine;
using ScriptableObject;
using UnityEngine;

namespace CameraSettings
{
    public class CameraSettingCalculator : MonoBehaviour
    {
        [SerializeField] private GridProperties gridProperties;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private float _resValue;
    
        private void Start()
        {
            int width = Display.main.systemWidth;
            int height = Display.main.systemHeight;
            _resValue = (float)height / width;
            SetCameraSetting(gridProperties.Space);
        }
        public void SetCameraSetting(float value)
        {
            Vector3 pos = new Vector3(-value*(gridProperties.GridSize - 1)/2, value*(gridProperties.GridSize - 1)/2,1);
            virtualCamera.transform.position = pos;
            virtualCamera.m_Lens.OrthographicSize = _resValue * value * gridProperties.GridSize / 2;
        }
    }
}