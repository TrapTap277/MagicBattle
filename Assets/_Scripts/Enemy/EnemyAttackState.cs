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
            await Task.Delay(2000);
            if (_isExit) return;
            var percentToAttack = (float) enemyStateMachine.Storage.BlueAttack / enemyStateMachine.Storage.AttackCount *
                                  100;
            var randomNumber = Random.Range(0, 100);

            if (randomNumber < percentToAttack)
                enemyStateMachine.ShotInvoker.ShootInYou(true);
            else
                enemyStateMachine.ShotInvoker.ShootInEnemy(true);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            _isExit = true;
        }
    }
}