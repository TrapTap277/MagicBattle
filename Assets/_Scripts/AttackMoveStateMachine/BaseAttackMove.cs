using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public abstract class BaseAttackMove
    {
        public abstract void Enter(StateMachine stateMachine);
        public abstract void Exit(StateMachine stateMachine);
    }
}