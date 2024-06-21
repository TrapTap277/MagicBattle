using System;
using _Scripts.LostScene;

namespace _Scripts.Animations
{
    public static class AnimationSwitcher<TEnum, TSwitcher> 
        where TEnum : Enum 
        where TSwitcher : ISwitchAnimation<TEnum>
    {
        public static void SetRandomAnimation(TSwitcher switcher)
        {
            var random = switcher.SetRandomAttackAnimation();
            switcher.SwitchAnimation(random);
        }

        public static void SwitchAnimation(TSwitcher switcher, TEnum animation)
        {
            switcher.SwitchAnimation(animation);
        }
    }
}