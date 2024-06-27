using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _Scripts.MainMenu
{
    public class VignetteLerp : MonoBehaviour
    {
        public static event Action<int> OnChangedScene;
        public static event Action OnFadeButtons;

        private const float LerpDuration = 1000f;
        private static float _startIntensity = 0.4f;
        private static float _endIntensity = 0.6f;
        private float _lerpTimer;
        private Vignette _vignette;

        private bool _isStartedGame;

        public void ShowVignette()
        {
            SetVignette();

            SetIsStartedGame(false);

            StartCoroutine(LerpVignetteRoutine());
        }

        public void StartGame()
        {
            SetIsStartedGame(true);

            StartCoroutine(ChangeScene());
        }

        public IEnumerator FadeVignette()
        {
            while (true)
            {
                if (!_isStartedGame)
                {
                    _lerpTimer += Time.deltaTime;
                    LerpVignette(_vignette.intensity.value, 0, _lerpTimer / LerpDuration);

                    if (_lerpTimer >= LerpDuration)
                    {
                        _lerpTimer = 0f;
                        (_startIntensity, _endIntensity) = (_endIntensity, _startIntensity);
                    }
                }

                yield return null;
            }
        }

        private IEnumerator LerpVignetteRoutine()
        {
            _lerpTimer = 0;
            while (true)
            {
                if (!_isStartedGame)
                {
                    _lerpTimer += Time.deltaTime;
                    LerpVignette(_startIntensity, _endIntensity, _lerpTimer / LerpDuration);

                    if (_lerpTimer >= LerpDuration)
                    {
                        _lerpTimer = 0f;
                        (_startIntensity, _endIntensity) = (_endIntensity, _startIntensity);
                    }
                }
                else
                {
                    _lerpTimer += Time.deltaTime / 10;
                    LerpVignette(_vignette.intensity.value, 0.9f, _lerpTimer / 1000);
                }

                yield return null;
            }
        }


        private static IEnumerator ChangeScene()
        {
            OnFadeButtons?.Invoke();
            yield return new WaitForSeconds(3f);
            OnChangedScene?.Invoke(1);
        }

        private void SetIsStartedGame(bool isStarted)
        {
            _isStartedGame = isStarted;
        }

        private void SetVignette()
        {
            var postProcessVolume = GetComponent<PostProcessVolume>();
            _vignette = postProcessVolume.profile.GetSetting<Vignette>();
        }

        private void LerpVignette(float startIntensity, float endIntensity, float result)
        {
            var lerp = Mathf.Lerp(startIntensity, endIntensity, result);
            _vignette.intensity.value = lerp;
        }
    }
}