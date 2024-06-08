using _Scripts.AttackMoveStateMachine;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        #region States
        
        public readonly EnemyIdleState IdleState = new EnemyIdleState();
        public readonly EnemyAttackState AttackState = new EnemyAttackState();
        public readonly EnemyUseItemState UseItemState = new EnemyUseItemState();
        private EnemyBaseState _enemyCurrentState;

        #endregion

        public MoveTransition MoveTransition;
        public ShootInvoker ShotInvoker;
        public MagicAttackStorage Storage;
        
        private void Awake()
        {
            _enemyCurrentState = IdleState;
            _enemyCurrentState.Enter(this);
        }

        public void SwitchState(EnemyBaseState state)
        {
            _enemyCurrentState.Exit(this);
            _enemyCurrentState = state;
            _enemyCurrentState.Enter(this);
        }
    }
}