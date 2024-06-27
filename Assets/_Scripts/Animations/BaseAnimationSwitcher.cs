using UnityEngine;

namespace _Scripts.Animations
{
    public abstract class BaseAnimationSwitcher : MonoBehaviour
    {
        private Animator _animator;
        protected abstract void InitHashes();

        protected abstract void InitDictionary();

        protected abstract void InitList();

        protected void CrossFade(int randomNumber, float timeToTransition = 0)
        {
            _animator.CrossFade(randomNumber, timeToTransition);
        }

        protected void Init(Animator animator)
        {
            InitAnimator(animator);
            InitList();
            InitHashes();
            InitDictionary();
        }

        private void InitAnimator(Animator animator)
        {
            _animator = animator;
        }
    }
}