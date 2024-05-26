namespace _Scripts.Enemy
{
    public abstract class EnemyBaseState
    {
        public abstract void Enter(EnemyStateMachine enemyStateMachine);
        public abstract void Exit(EnemyStateMachine enemyStateMachine);
    }
}