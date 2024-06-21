using System;

namespace _Scripts.LostScene
{
    public interface ISwitchAnimation<T> where T : Enum
    {
        void SwitchAnimation(T staffAnimations);
        T SetRandomAttackAnimation();
    }
}