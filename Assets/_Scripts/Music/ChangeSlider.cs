using System;
using UnityEngine;

namespace _Scripts.Music
{
    public class ChangeSlider : MonoBehaviour
    {
        public static event Action<float> OnChangedSlider;

        public void ChangeMusicVolume(float volume)
        {
            float volumeInPercents = volume / 100;

            OnChangedSlider?.Invoke(volumeInPercents);
        }
    }
}