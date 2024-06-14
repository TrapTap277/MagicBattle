using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class SwitchEnemyStateWithShootingInEnemy : IEnemyStateSwitcher
    {
        private readonly bool _isEnemy;
        private SecondMoveTurn _secondMoveTurn;
        private readonly MagicAttackStorage _storage;
        private readonly EnemyStateMachine _stateMachine;

        private const MoveTurn EnemyTurn = MoveTurn.Enemy;
        private const MoveTurn PlayerTurn = MoveTurn.Player;
        private const SecondMoveTurn NoneSecondMove = SecondMoveTurn.None;
        private const SecondMoveTurn EnemySecondMove = SecondMoveTurn.Enemy;

        public SwitchEnemyStateWithShootingInEnemy(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public SwitchEnemyStateWithShootingInEnemy(bool isEnemy, SecondMoveTurn secondMoveTurn,
            EnemyStateMachine stateMachine, MagicAttackStorage storage)
        {
            _isEnemy = isEnemy;
            _secondMoveTurn = secondMoveTurn;
            _stateMachine = stateMachine;
            _storage = storage;
        }

        public void SwitchState(int attackIndex)
        {
            if (attackIndex == 0)
            {
                if (_secondMoveTurn == NoneSecondMove && !_isEnemy)
                {
                    GiveMoveToEnemy(EnemyTurn);
                    return;
                }

                if (_secondMoveTurn == NoneSecondMove && _isEnemy)
                {
                    GiveMoveToEnemy(PlayerTurn);
                    return;
                }

                if (_secondMoveTurn == EnemySecondMove && _isEnemy)
                {
                    GiveMoveToEnemy(EnemyTurn);
                    return;
                }
            }

            if (attackIndex >= 1)
            {
                if (_secondMoveTurn == NoneSecondMove)
                {
                    GiveMoveToEnemy(EnemyTurn);
                    return;
                }

                if (_secondMoveTurn != EnemySecondMove || !_isEnemy) return;
                GiveMoveToEnemy(EnemyTurn);
            }
        }

        public void ResetSecondMove()
        {
            _secondMoveTurn = SecondMoveTurn.None;
        }

        private void GiveMoveToEnemy(MoveTurn moveTurn)
        {
            if (_storage.AttackCount <= 1)
            {
                if(_stateMachine.MoveTurn == PlayerTurn) return; // Todo Block staff appearance if ended round
                _stateMachine.SetMoveTurn(MoveTurn.Player);
                _stateMachine.SwitchState(_stateMachine.IdleState);
                return;
            }

            _stateMachine.SetMoveTurn(moveTurn);
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }
    }
}