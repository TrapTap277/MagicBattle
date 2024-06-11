using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _Scripts.MainMenu
{
    public class VignetteLerp : MonoBehaviour
    {
        public static event Action<int> OnChengedScene;

        public float lerpDuration = 5f;
        private float lerpTimer;
        private Vignette vignette;
        private float startIntensity = 0.4f;
        private float endIntensity = 0.6f;

        private bool _isStartedGame;

        private void Start()
        {
            PostProcessVolume postProcessVolume = GetComponent<PostProcessVolume>();
            vignette = postProcessVolume.profile.GetSetting<Vignette>();

            _isStartedGame = false;
        }

        public void StartGame()
        {
            _isStartedGame = true;
        }

        private void Update()
        {
            if (!_isStartedGame)
            {
                lerpTimer += Time.deltaTime;
                var lerpedIntensity = Mathf.Lerp(startIntensity, endIntensity, lerpTimer / lerpDuration);
                vignette.intensity.value = lerpedIntensity;

                if (lerpTimer >= lerpDuration)
                {
                    lerpTimer = 0f;
                    (startIntensity, endIntensity) = (endIntensity, startIntensity);
                }
            }
            else
            {
                lerpTimer += Time.deltaTime / 10;
                var lerpedIntensity = Mathf.Lerp(vignette.intensity.value, 0.9f, lerpTimer / 1000);
                vignette.intensity.value = lerpedIntensity;

                StartCoroutine(ChangeScene());
            }
        }

        private IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(7f);

            OnChengedScene?.Invoke(1);
        }
    }
}