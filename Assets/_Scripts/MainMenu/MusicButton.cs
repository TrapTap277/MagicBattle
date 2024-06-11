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
            Init();

            _musicButton.onClick.AddListener(OnChangedSprite);
        }

        private void Init()
        {
            _currentSprite = GetComponent<Image>();
            _musicButton = GetComponent<Button>();
        }

        private void OnChangedSprite()
        {
            if (_spriteIndex == _buttonsSprites.Count) _spriteIndex = 0;

            _currentSprite.sprite = _buttonsSprites[_spriteIndex];

            OnChangeMusicVolume(_spriteIndex != 0);

            _spriteIndex++;
        }

        private static void OnChangeMusicVolume(bool change)
        {
            OnDisableOrEnableMusic?.Invoke(change);
        }
    }
}