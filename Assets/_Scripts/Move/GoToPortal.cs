using System;
using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.LostScene;
using _Scripts.Staff;
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
        private int _step;

        private void Start()
        {
            _passingLevel = FindObjectOfType<PassingLevel>();

            GoThroughLevel();
        }

        private async void GoThroughLevel()
        {
            foreach (var point in _path)
            {
                await AddStep();
                await MoveAndRotateCamera(point);
                await SetAnimation();
                await OpenDoor();

                if (_step != 5) continue;
                OnChangedScene?.Invoke(2);
            }
        }

        private async Task AddStep()
        {
            await Task.Delay(1000);
            _step++;
        }

        private async Task OpenDoor()
        {
            if (_step == 1 || _step == 2)
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
            if (_step > 4) return;

            switch (_step)
            {
                case 3:
                    _passingLevel.SetStaffAnimation(StaffAnimations.OpenPortal);
                    await Task.Delay(8500);
                    _passingLevel.FadeOrShowPortal(true);
                    return;
                case 4:
                    _passingLevel.SetStaffAnimation(StaffAnimations.FadeStaff);
                    await Task.Delay(1000);
                    return;
                case 1:
                    _passingLevel.SetStaffAnimation(StaffAnimations.ShowStaff);
                    await Task.Delay(2000);
                    break;
            }
            
            _passingLevel.SetStaffAnimation(StaffAnimations.None);
            await Task.Delay(3000);
        }
    }
}