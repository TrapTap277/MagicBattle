using _Scripts.Items;

namespace _Scripts.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private bool _isDone;

        public override void Enter(EnemyStateMachine enemyStateMachine)
        {
            if (enemyStateMachine.MoveTurn != MoveTurn.Enemy || enemyStateMachine.IsStopped)
            {
                if (enemyStateMachine.IsDied == false)
                    enemyStateMachine.MoveTransition.TransitionToPlayer();
                
                enemyStateMachine.UsedItems.Clear();
                _isDone = false;
                return;
            }

            if (!_isDone)
            {
                enemyStateMachine.MoveTransition.TransitionToEnemy();
                _isDone = true;
            }

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