using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private bool _isExit;

        public override async void Enter(EnemyStateMachine enemyStateMachine)
        {
            _isExit = false;
            enemyStateMachine.MoveTransition.TransitionToEnemy();
            await Task.Delay(5000);
            if (_isExit) return;
            var percentToAttack = (float) enemyStateMachine.Storage.BlueAttack / enemyStateMachine.Storage.AttackCount * 100;
            var randomNumber = Random.Range(0, 100);
            if (randomNumber < percentToAttack)
                enemyStateMachine.ShotInvoker.ShootInYou(true);
            else
                enemyStateMachine.ShotInvoker.ShootInEnemy(true);
            await Task.Delay(2000);
            enemyStateMachine.SwitchState(enemyStateMachine.IdleState);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            enemyStateMachine.MoveTransition.TransitionToPlayer();
            _isExit = true;
        }
    }
}