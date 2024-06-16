using System.Threading.Tasks;
using _Scripts.Die;
using _Scripts.LostScene;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.EndGame
{
    public class PlayDemonicEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _demonicEffect;

        [SerializeField] private Transform _effectPositions;

        private IChangeAnimation _changeAnimation;

        private void Start()
        {
            _changeAnimation = FindObjectOfType<Darkness>();
        }

        private async void Play()
        {
            var newDemonicEffect = Instantiate(_demonicEffect, _effectPositions);
            newDemonicEffect.transform.DOScale(30, 10);

            await Task.Delay(5000);

            _changeAnimation?.CrossFade();
        }

        private void OnEnable()
        {
            DieCounter.OnPlayedDemonicEffect += Play;
        }

        private void OnDisable()
        {
            DieCounter.OnPlayedDemonicEffect -= Play;
        }
    }
}