using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace _Scripts.BoxWithItems
{
    public class CreateBox : MonoBehaviour
    {
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private Transform _mainCameraTranform;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _endPosition;

        private Quaternion _startCameraTransform;
        private Transform _boxPositions;
        private void Awake()
        {
            _startCameraTransform = _mainCameraTranform.rotation;
            
            CreateAndMove();
        }

        public void CreateAndMove()
        {
            GameObject box = Instantiate(_boxPrefab, _spawnPosition.position, _spawnPosition.rotation);
            _boxPositions = box.transform;
            MoveUp(box.transform);
        }

        public void ExitFromBox()
        {
            MoveDown(_boxPositions);
        }

        private void MoveDown(Transform box)
        {
            Sequence move = DOTween.Sequence();
            move.Append(box.DOMoveY(_spawnPosition.position.y, 3).SetEase(Ease.Linear).OnComplete(ChangeCameraRotationToDefault));
        }

        private void MoveUp(Transform box)
        {
            Sequence move = DOTween.Sequence();
            move.Append(box.DOMoveY(_endPosition.position.y, 3).SetEase(Ease.Linear).OnComplete(ChangeCameraRotation));
        }

        private void ChangeCameraRotationToDefault()
        {
            _mainCameraTranform.DORotate(_startCameraTransform.eulerAngles, 3).SetEase(Ease.Linear);
        }

        private void ChangeCameraRotation()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_endPosition.position - _mainCameraTranform.position);
            _mainCameraTranform.DORotateQuaternion(lookRotation, 3).SetEase(Ease.Linear);
        }
    }
}