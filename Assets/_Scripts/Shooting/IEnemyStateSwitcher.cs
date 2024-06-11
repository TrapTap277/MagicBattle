namespace _Scripts.Shooting
{
    public interface IEnemyStateSwitcher
    {
        void SwitchState(int index);
        void ResetSecondMove();
    }
}