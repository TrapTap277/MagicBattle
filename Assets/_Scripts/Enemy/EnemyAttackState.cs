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
            if (_isExit) return;

            if (enemyStateMachine.RandomNumber < enemyStateMachine.PercentToAttackInPlayer)
                enemyStateMachine.ShotInvoker.ShootInPlayer(true);
            else
                enemyStateMachine.ShotInvoker.ShootInEnemy(true);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            _isExit = true;
        }
    }
}