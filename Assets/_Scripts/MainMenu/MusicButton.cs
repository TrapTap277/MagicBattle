using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MainMenu
{
    public class MusicButton : MonoBehaviour
    {
        public static event Action<bool> OnDisableOrEnableMusic;

        [SerializeField] private List<Sprite> _buttonsSprites = new List<Sprite>();

        private Button _musicButton;
        private Image _currentSprite;

        private int _spriteIndex;

        private void Start()
        {
            _currentSprite = GetComponent<Image>(); 
            _musicButton = GetComponent<Button>();

            _musicButton.onClick.AddListener(OnChangedSprite);
        }

        public void OnChangedSprite()
        {
            if(_spriteIndex == _buttonsSprites.Count)
                _spriteIndex = 0;

            _currentSprite.sprite = _buttonsSprites[_spriteIndex];

            if (_spriteIndex == 0)
                OnChangeMusicVolume(false);

            else
            {
                OnChangeMusicVolume(true);
            }

            _spriteIndex++;
        }

        private void OnChangeMusicVolume(bool change)
        {
            OnDisableOrEnableMusic?.Invoke(change);
        }
    }
}