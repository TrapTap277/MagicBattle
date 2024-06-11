using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Music
{
    public class ChangeSlider : MonoBehaviour
    {
        public static event Action<float> OnChangedSlider;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.onValueChanged.AddListener(ChangeMusicVolume);
        }

        public void ChangeMusicVolume(float volume)
        {
            var volumeInPercents = volume / 100;

            OnChangedSlider?.Invoke(volumeInPercents);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(ChangeMusicVolume);
        }
    }
}