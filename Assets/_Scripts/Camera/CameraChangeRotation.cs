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
            var cameraPrefab = gameObject;
            _startCameraRotation = cameraPrefab.transform.rotation;
            _startCameraTransform = cameraPrefab.transform;
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
            BoxWithItems.MoveBox.OnChangedCameraRotationToDefault += ChangeCameraRotationToDefault;
            BoxWithItems.MoveBox.OnChangedCameraRotation += ChangeCameraRotation;
        }

        private void OnDisable()
        {
            BoxWithItems.MoveBox.OnChangedCameraRotationToDefault -= ChangeCameraRotationToDefault;
            BoxWithItems.MoveBox.OnChangedCameraRotation -= ChangeCameraRotation;
        }
    }
}