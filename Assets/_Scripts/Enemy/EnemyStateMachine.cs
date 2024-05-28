using System;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        #region States

        private EnemyBaseState _enemyCurrentState;

        public EnemyIdleState IdleState = new EnemyIdleState();

        public EnemyAttackState AttackState = new EnemyAttackState();

        public EnemyUseItemState UseItemState = new EnemyUseItemState();

        #endregion

        public MoveTransition MoveTransition;
        public ShootButtons ShootButtons;
        public MagicAttackStorage Storage;
        
        private void Start()
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