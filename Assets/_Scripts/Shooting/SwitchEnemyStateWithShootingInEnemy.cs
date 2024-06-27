using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class SwitchEnemyStateWithShootingInEnemy : IEnemyStateSwitcher
    {
        private readonly MoveTurn _moveTurn;
        private SecondMoveTurn _secondMoveTurn;
        private readonly MagicAttackStorage _storage;
        private readonly EnemyStateMachine _stateMachine;

        private const MoveTurn EnemyTurn = MoveTurn.Enemy;
        private const MoveTurn PlayerTurn = MoveTurn.Player;
        private const SecondMoveTurn NoneSecondMove = SecondMoveTurn.None;
        private const SecondMoveTurn EnemySecondMove = SecondMoveTurn.Enemy;

        public SwitchEnemyStateWithShootingInEnemy(MoveTurn moveTurn, SecondMoveTurn secondMoveTurn,
            EnemyStateMachine stateMachine, MagicAttackStorage storage)
        {
            _moveTurn = moveTurn;
            _secondMoveTurn = secondMoveTurn;
            _stateMachine = stateMachine;
            _storage = storage;
        }

        public void SwitchState(int attackIndex)
        {
            if (attackIndex == 0)
            {
                switch (_secondMoveTurn)
                {
                    case NoneSecondMove when _moveTurn == PlayerTurn:
                        GiveMoveToEnemy(EnemyTurn);
                        return;
                    case NoneSecondMove when _moveTurn == EnemyTurn:
                        GiveMoveToEnemy(PlayerTurn);
                        return;
                    case EnemySecondMove when _moveTurn == EnemyTurn:
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

                if (_secondMoveTurn != EnemySecondMove || _moveTurn == PlayerTurn) return;
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