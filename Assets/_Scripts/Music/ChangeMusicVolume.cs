using System.Collections.Generic;
using System.Linq;
using _Scripts.MainMenu;
using UnityEngine;

namespace _Scripts.Music
{
    public class ChangeMusicVolume : MonoBehaviour
    {
        private const string MusicVolumeKey = "MusicVolume";
        private const string MusicActiveKey = "MusicActive";
        private const string MusicTag = "Music";

        private readonly List<AudioSource> _music = new List<AudioSource>();

        private void Start()
        {
            var musicSources = FindObjectsOfType<AudioSource>();
            foreach (var audioSource in musicSources)
                if (audioSource.CompareTag(MusicTag))
                    _music.Add(audioSource);

            var savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            var musicActive = PlayerPrefs.GetInt(MusicActiveKey, 1) == 1;
            OnChangeMusicVolume(savedVolume);
            OnDisableOrEnableMusic(musicActive);
        }

        private void OnChangeMusicVolume(float volume)
        {
            foreach (var audioSource in _music.Where(audioSource => audioSource != null))
                audioSource.volume = volume;

            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        }

        private void OnDisableOrEnableMusic(bool isActive)
        {
            foreach (var audioSource in _music.Where(audioSource => audioSource != null))
                audioSource.gameObject.SetActive(isActive);

            PlayerPrefs.SetInt(MusicActiveKey, isActive ? 1 : 0);
        }

        private void OnEnable()
        {
            ChangeSlider.OnChangedSlider += OnChangeMusicVolume;
            MusicButton.OnDisableOrEnableMusic += OnDisableOrEnableMusic;
        }

        private void OnDisable()
        {
            ChangeSlider.OnChangedSlider -= OnChangeMusicVolume;
            MusicButton.OnDisableOrEnableMusic -= OnDisableOrEnableMusic;
        }
    }
}