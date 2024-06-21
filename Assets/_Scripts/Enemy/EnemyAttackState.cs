using System.Threading.Tasks;

namespace _Scripts.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private bool _isExit;

        public override async void Enter(EnemyStateMachine enemyStateMachine)
        {
            _isExit = false;
            await Task.Delay(2000);

            if (_isExit || enemyStateMachine.IsStopped) return;

            // var randomAttack = enemyStateMachine.EnemySwitchAnimation.SetRandomAttackAnimation();
            // enemyStateMachine.EnemySwitchAnimation.SwitchAnimation(randomAttack);

            if (enemyStateMachine.RandomNumber < enemyStateMachine.PercentToAttackInPlayer)
                enemyStateMachine.ShotInvoker.ShootInPlayer(MoveTurn.Enemy);
            else
                enemyStateMachine.ShotInvoker.ShootInEnemy(MoveTurn.Enemy);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            _isExit = true;
        }
    }
}