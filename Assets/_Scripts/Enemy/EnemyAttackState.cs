using System.Threading.Tasks;
using _Scripts.Shooting;

namespace _Scripts.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        public override async void Enter(EnemyStateMachine enemyStateMachine)
        {
            await Task.Delay(5000);

            float percentToAttack = (float)enemyStateMachine.Storage.RedAttack / 
                                    enemyStateMachine.Storage.AttackCount * 100;
            int randomNumber = UnityEngine.Random.Range(0, 100);

            if (randomNumber < percentToAttack)
            {
                IShoot shootInEnemy = new ShootInEnemy(enemyStateMachine.StaffAnimator, enemyStateMachine.Storage);
            }

            else
            {
                IShoot shootInPlayer = new ShootInPlayer(enemyStateMachine.StaffAnimator, enemyStateMachine.Storage);
            }

        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            
        }
    }
}