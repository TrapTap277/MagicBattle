using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;

namespace _Scripts.Shooting
{
    public class SwitchEnemyStateWithShootingInPlayer : IEnemyStateSwitcher
    {
        private readonly MoveTurn _moveTurn;
        private SecondMoveTurn _currentSecondMoveTurn;
        private readonly MagicAttackStorage _storage;
        private readonly EnemyStateMachine _stateMachine;

        private const MoveTurn EnemyTurn = MoveTurn.Enemy;
        private const MoveTurn PlayerTurn = MoveTurn.Player;
        private const SecondMoveTurn NoneSecondMove = SecondMoveTurn.None;
        private const SecondMoveTurn PlayerSecondMove = SecondMoveTurn.Player;
        private const SecondMoveTurn EnemySecondMove = SecondMoveTurn.Enemy;

        public SwitchEnemyStateWithShootingInPlayer(MoveTurn moveTurn, SecondMoveTurn currentSecondMoveTurn,
            EnemyStateMachine stateMachine, MagicAttackStorage storage)
        {
            _moveTurn = moveTurn;
            _currentSecondMoveTurn = currentSecondMoveTurn;
            _stateMachine = stateMachine;
            _storage = storage;
        }

        public void SwitchState(int attackIndex)
        {
            if (attackIndex == 0)
            {
                switch (_currentSecondMoveTurn)
                {
                    case NoneSecondMove when _moveTurn == PlayerTurn:
                        GiveMoveToEnemy(EnemyTurn);

                        return;
                    case NoneSecondMove when _moveTurn == EnemyTurn:
                        GiveMoveToEnemy(PlayerTurn);

                        return;
                    case PlayerSecondMove:
                        GiveMoveToEnemy(PlayerTurn);

                        return;
                    case EnemySecondMove when _moveTurn == EnemyTurn:
                        GiveMoveToEnemy(EnemyTurn);
                        return;
                }
            }

            if (attackIndex < 1) return;
            switch (_currentSecondMoveTurn)
            {
                case EnemySecondMove when _moveTurn == EnemyTurn:
                    GiveMoveToEnemy(EnemyTurn);
                    return;
                case PlayerSecondMove:
                    GiveMoveToEnemy(PlayerTurn);
                    return;
            }

            if (_moveTurn == PlayerTurn) return;
            GiveMoveToEnemy(PlayerTurn);
        }

        public void ResetSecondMove()
        {
            _currentSecondMoveTurn = SecondMoveTurn.None;
        }

        private void GiveMoveToEnemy(MoveTurn moveTurn)
        {
            if (_storage.AttackCount <= 1)
            {
                if(_stateMachine.MoveTurn == PlayerTurn) return;  // Todo Block staff appearance if ended round
                _stateMachine.SetMoveTurn(MoveTurn.Player);
                _stateMachine.SwitchState(_stateMachine.IdleState);
                return;
            }

            _stateMachine.SetMoveTurn(moveTurn);
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }
    }
}