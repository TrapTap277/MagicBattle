using System.Collections.Generic;
using _Scripts.MainMenu;
using UnityEngine;

namespace _Scripts.Music
{
    public class ChangeMusicVolume : MonoBehaviour
    {
        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string MUSIC_ACTIVE_KEY = "MusicActive";

        private List<AudioSource> _music;

        private void Start()
        {
            _music = new List<AudioSource>();

            AudioSource[] musicSources = GameObject.FindObjectsOfType<AudioSource>();
            foreach (var source in musicSources)
            {
                if (source.CompareTag("Music"))
                {
                    _music.Add(source);
                }
            }

            float savedVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
            bool musicActive = PlayerPrefs.GetInt(MUSIC_ACTIVE_KEY, 1) == 1;
            OnChangeMusicVolume(savedVolume);
            OnDisableOrEnableMusic(musicActive);
        }

        public void OnChangeMusicVolume(float volume)
        {
            foreach (var audio in _music)
            {
                if (audio != null)
                    audio.volume = volume;
            }


            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }

        public void OnDisableOrEnableMusic(bool isActive)
        {
            foreach (var audio in _music)
            {
                if (audio != null)
                    audio.gameObject.SetActive(isActive);
            }

            PlayerPrefs.SetInt(MUSIC_ACTIVE_KEY, isActive ? 1 : 0);
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