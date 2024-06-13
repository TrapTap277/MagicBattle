﻿using System;
using System.Threading.Tasks;
using _Scripts.LostScene;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Move
{
    public class GoToPortal : MonoBehaviour
    {
        public static event Action<int> OnChangedScene;

        [SerializeField] private Transform[] _path;

        private PassingLevel _passingLevel;

        private const float TimeToGo = 2f;
        private int _move;

        private void Start()
        {
            _passingLevel = FindObjectOfType<PassingLevel>();

            GoThroughLevel();
        }

        private async void GoThroughLevel()
        {
            foreach (var point in _path)
            {
                await Task.Delay(1000);
                _move++;

                await MoveAndRotateCamera(point);
                await SetAnimation();
                await OpenDoor();

                if (_move == 3)
                {
                    _passingLevel.FadeOrShowPortal(true);
                    await Task.Delay(2000);
                }

                if (_move != 5) continue;
                OnChangedScene?.Invoke(2);
            }
        }

        private async Task OpenDoor()
        {
            if (_move == 1 || _move == 2)
            {
                await Task.Delay(2000);
                await _passingLevel.DestroyDoorAndUseMagicAsync();
            }
        }

        private async Task MoveAndRotateCamera(Transform point)
        {
            var move = DOTween.Sequence();
            move.Append(gameObject.transform.DOMove(point.position, TimeToGo).SetEase(Ease.Linear));
            await Task.Delay(2000);
            move.Append(gameObject.transform.DORotate(Vector3.zero, 1f).SetEase(Ease.Flash));
            await Task.Delay(1000);
        }

        private async Task SetAnimation()
        {
            Debug.LogWarning(_move);
            if (_move > 4) return;

            if (_move == 3)
            {
                await _passingLevel.SetCallPortalAnimation();
                return;
            }
            
            if (_move == 4)
            {
                _passingLevel.SetFadeAnimation();
                await Task.Delay(1000);
                return;
            }

            if (_move == 1)
            {
                _passingLevel.SetShowAnimation();
                await Task.Delay(2000);
            }
            _passingLevel.SetRandomAttackAnimation();
            await Task.Delay(3000);
        }
    }
}