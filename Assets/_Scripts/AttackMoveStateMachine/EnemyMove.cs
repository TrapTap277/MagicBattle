using DG.Tweening;

namespace _Scripts.AttackMoveStateMachine
{
    public class EnemyMove : BaseAttackMove
    {
        public override void Enter(StateMachine stateMachine)
        {
            Sequence move = DOTween.Sequence();

            move.Append(stateMachine.Staff.DOMove(stateMachine.EndStaffPosition  .position, 2f));
            move.Append(stateMachine.AttackButtons.DOFade(0, 2));
        }

        public override void Exit(StateMachine stateMachine)
        {            
            Sequence move = DOTween.Sequence();
            
            move.Append(stateMachine.Staff.DOMove(stateMachine.StartStaffPosition.position, 2f));
            move.Append(stateMachine.AttackButtons.DOFade(1, 2));
        }
    }
}