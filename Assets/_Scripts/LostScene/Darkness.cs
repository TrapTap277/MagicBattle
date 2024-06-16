using UnityEngine;

namespace _Scripts.LostScene
{
    public class Darkness : MonoBehaviour, IChangeAnimation
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void CrossFade()
        {
            _animator.CrossFade("DarknessShow", 0);
        }
    }
}