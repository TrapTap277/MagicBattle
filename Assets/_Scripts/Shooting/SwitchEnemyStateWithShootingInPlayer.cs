using _Scripts.Enemy;
using _Scripts.Items;

namespace _Scripts.Shooting
{
    public class SwitchEnemyStateWithShootingInPlayer : IEnemyStateSwitcher
    {
        private readonly bool _isEnemy;
        private readonly SecondMoveTurn _secondMoveTurn;
        private readonly EnemyStateMachine _stateMachine;

        public SwitchEnemyStateWithShootingInPlayer(bool isEnemy, SecondMoveTurn secondMoveTurn, EnemyStateMachine stateMachine)
        {
            _isEnemy = isEnemy;
            _secondMoveTurn = secondMoveTurn;
            _stateMachine = stateMachine;
        }


        public void SwitchState(int attackIndex)
        {
            if (attackIndex == 0)
            {
                if (_secondMoveTurn == SecondMoveTurn.None && !_isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
                
                if (_secondMoveTurn == SecondMoveTurn.Enemy && _isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
            }

            if (attackIndex >= 1)
            {
                if(_secondMoveTurn == SecondMoveTurn.None && _isEnemy)
                    _stateMachine.SwitchState(_stateMachine.IdleState);
                
                if(_secondMoveTurn == SecondMoveTurn.Enemy && _isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
            }
        }
    }
}