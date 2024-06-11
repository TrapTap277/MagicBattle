using _Scripts.Items;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public override void Enter(EnemyStateMachine enemyStateMachine)
        {
            if (enemyStateMachine.MoveTurn != MoveTurn.Enemy)
            {
                enemyStateMachine.MoveTransition.TransitionToPlayer();
                enemyStateMachine.UsedItems.Clear();
                
                return;
            }

            enemyStateMachine.MoveTransition.TransitionToEnemy();
            enemyStateMachine.CalculatePercent();
            
            if (GenerateRandomItem.ItemsCount > 0)
                enemyStateMachine.SwitchState(enemyStateMachine.UseItemState);

            else
                enemyStateMachine.SwitchState(enemyStateMachine.AttackState);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
        }
    }
}