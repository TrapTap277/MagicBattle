using System.Collections;
using UnityEngine;

namespace _Scripts.DisableGameObject
{
    public class DisableGameObjectWithTime : MonoBehaviour
    {
        [SerializeField] private float TIME_TO_FADE = 10;

        private void Start()
        {
            StartCoroutine(Fade(TIME_TO_FADE));
        }

        private IEnumerator Fade(float time)
        {
            yield return new WaitForSeconds(time);

            gameObject.SetActive(false);
        }
    }
}