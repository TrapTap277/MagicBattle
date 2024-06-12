using UnityEngine;

namespace _Scripts.LostScene
{
    public class Darkness : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void CrossFadeToDarknessShow()
        {
            _animator.CrossFade("DarknessShow", 0);
        }
    }
}