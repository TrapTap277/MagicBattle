using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInEnemy : BaseShoot, IShoot
    {
        private readonly IEnemyStateSwitcher _enemyStateSwitcher;

        public ShootInEnemy(MagicAttackStorage attackStorage,EnemyStateMachine stateMachine, SecondMoveTurn secondMoveTurn, bool isEnemy) : base(attackStorage)
        {
            _enemyStateSwitcher = new SwitchEnemyStateWithShootingInEnemy(isEnemy, secondMoveTurn, stateMachine, attackStorage);
        }

        public override void Shoot()
        {
            ShootBase(ShootIn.Enemy, _enemyStateSwitcher);
            
            // Debug.LogError($"Shoot in {GetType().Name}");
        }
    }
}