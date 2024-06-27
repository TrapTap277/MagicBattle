using System;

namespace _Scripts.LostScene
{
    public interface ISwitchAnimation<T> where T : Enum
    {
        void SwitchAnimation(T staffAnimations, float timeToTransition = 0);
        T SetRandomAttackAnimation();
    }
}