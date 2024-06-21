using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Animations
{
    public class Enemy : MonoBehaviour
    {
        private EnemyAnimationSwitcher _animationSwitcher;
        private readonly ISwitchAnimation<StaffAnimations> _switchAnimation;


        private void Start()
        {
            _animationSwitcher = GetComponent<EnemyAnimationSwitcher>();
        }

        public void PerformRandomAttack()
        {
            AnimationSwitcher<EnemyAnimations, ISwitchAnimation<EnemyAnimations>>
                .SwitchAnimation(_animationSwitcher, EnemyAnimations.None); 
        }
    }
}