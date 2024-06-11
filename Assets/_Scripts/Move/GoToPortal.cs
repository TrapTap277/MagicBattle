using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Move
{
    public class GoToPortal : MonoBehaviour
    {
        public static event Action<int> OnChangedScene;
        public static event Action<int> OnOpenedDoor;
        public static event Action OnOpenedPortal;

        [SerializeField] private Transform[] _path;

        private const float TIME_TO_GO = 2f;
        private int _moves;
        private int _doorIndex;

        void Start()
        {
            _doorIndex = 999999;

            StartCoroutine(Go());
        }

        public IEnumerator Go()
        {
            Sequence move = DOTween.Sequence();

            Vector3[] pathPositions = new Vector3[_path.Length];
            for (int i = 0; i < _path.Length; i++)
            {
                pathPositions[i] = _path[i].position;
            }

            foreach (var point in _path)
            {
                yield return new WaitForSeconds(1f);
                _moves++;

                _doorIndex = ChangeDoorIndex();

                move.Append(gameObject.transform.DOMove(point.position, TIME_TO_GO).SetEase(Ease.Linear));
                yield return new WaitForSeconds(2f);
                move.Append(gameObject.transform.DORotate(Vector3.zero, 1f).SetEase(Ease.Flash));

                if(_doorIndex == 0 || _doorIndex == 1)
                {
                    OnOpenedDoor?.Invoke(_doorIndex);

                    yield return new WaitForSeconds(4.5f);
                }

                if(_moves == 3)
                {
                    OnOpenedPortal?.Invoke();
                    yield return new WaitForSeconds(2);
                }

                if(_moves == 5)
                {
                    OnChangedScene?.Invoke(2);
                }
            }
        }

        private int ChangeDoorIndex()
        {
            if (_moves == 1)
                return 0;

            if (_moves == 2)
                return 1;

            return -1;
        }
    }
}