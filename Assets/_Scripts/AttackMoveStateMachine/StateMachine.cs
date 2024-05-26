using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private BaseAttackMove _currentAttackMove;

        public PlayerMove PlayerMove = new PlayerMove();
        public EnemyMove EnemyMove = new EnemyMove();

        public Transform Staff;
        public CanvasGroup AttackButtons;

        public Transform EndStaffPosition;
        public Transform StartStaffPosition;
        
        private void Start()
        {
            _currentAttackMove = PlayerMove;
            _currentAttackMove.Enter(this);    
        }

        public void SwitchState(BaseAttackMove attackMove)
        {
            _currentAttackMove.Exit(this);
            _currentAttackMove = attackMove;
            _currentAttackMove.Enter(this);
        }
    }
}