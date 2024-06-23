using System.Collections.Generic;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Animations
{
    public class GemAnimationSwitcher : BaseAnimationSwitcher, ISwitchAnimation<GemAnimations>
    {
        private const string DissolveStaff = "Dissolve";
        private const string UnDissolveStaff = "UnDissolve";

        private int _dissolveState;
        private int _unDissolveState;

        private Dictionary<GemAnimations, int> _gemAnimations = new Dictionary<GemAnimations, int>();
        private Animator _gemAnimator;

        private void Awake()
        {
            _gemAnimator = GetComponent<Animator>();

            Init(_gemAnimator);
        }

        public void SwitchAnimation(GemAnimations staffAnimations, float timeToTransition = 0)
        {
            var value = GetStaffAnimation(staffAnimations);
            CrossFade(value, timeToTransition);
        }

        protected override void InitHashes()
        {
            _dissolveState = Animator.StringToHash(DissolveStaff);
            _unDissolveState = Animator.StringToHash(UnDissolveStaff);
        }

        protected override void InitDictionary()
        {
            _gemAnimations = new Dictionary<GemAnimations, int>
            {
                {GemAnimations.Dissolved, _dissolveState},
                {GemAnimations.Undissolved, _unDissolveState}
            };
        }

        private int GetStaffAnimation(GemAnimations gemAnimations)
        {
            _gemAnimations.TryGetValue(gemAnimations, out var value);
            return value;
        }

        public GemAnimations SetRandomAttackAnimation()
        {
            return GemAnimations.None;
        }

        protected override void InitList()
        {
        }
    }
}