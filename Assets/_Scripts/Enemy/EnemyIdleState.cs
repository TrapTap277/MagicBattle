using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public override void Enter(EnemyStateMachine enemyStateMachine)
        {
            Debug.Log("Enter in Idle State");
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            
        }
    }
}