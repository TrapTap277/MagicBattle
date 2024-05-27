using System.Threading.Tasks;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        public override async void Enter(EnemyStateMachine enemyStateMachine)
        {
            enemyStateMachine.MoveTransition.TransitionToEnemy();
            
            await Task.Delay(5000);

            float percentToAttack = (float)enemyStateMachine.Storage.RedAttack / 
                                    enemyStateMachine.Storage.AttackCount * 100;
            int randomNumber = UnityEngine.Random.Range(0, 100);

            Debug.LogError(randomNumber);
            Debug.LogError(percentToAttack);
            
            if (randomNumber < percentToAttack)
            {
                enemyStateMachine.ShootButtons.ShootInYou(true);
            }

            else
            {     
                enemyStateMachine.ShootButtons.ShootInEnemy(true);
            }

            await Task.Delay(2000);
            
            enemyStateMachine.SwitchState(enemyStateMachine.IdleState);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            enemyStateMachine.MoveTransition.TransitionToPlayer();
            
            Debug.Log("Exit from attack state");
        }
    }
}