using DG.Tweening;
using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraChangeRotation : MonoBehaviour
    {
        private Transform _mainCameraTransform;
        private Transform _startCameraTransform;
        private Quaternion _startCameraRotation;

        private void Awake()
        {
            var camera = gameObject;
            _startCameraRotation = camera.transform.rotation;
            _startCameraTransform = camera.transform;
        }

        private void ChangeCameraRotation(Transform endPlayerPosition)
        {
            var lookRotation = Quaternion.LookRotation(endPlayerPosition.position - _startCameraTransform.position);
            gameObject.transform.DORotateQuaternion(lookRotation, 1.5f).SetEase(Ease.Linear);
        }

        private void ChangeCameraRotationToDefault()
        {
            gameObject.transform.DORotate(_startCameraRotation.eulerAngles, 1.5f).SetEase(Ease.Linear);
        }

        private void OnEnable()
        {
            BoxWithItems.BoxWithItems.OnChangedCameraRotationToDefault += ChangeCameraRotationToDefault;
            BoxWithItems.BoxWithItems.OnChangedCameraRotation += ChangeCameraRotation;
        }

        private void OnDisable()
        {
            BoxWithItems.BoxWithItems.OnChangedCameraRotationToDefault -= ChangeCameraRotationToDefault;
            BoxWithItems.BoxWithItems.OnChangedCameraRotation -= ChangeCameraRotation;
        }
    }
}