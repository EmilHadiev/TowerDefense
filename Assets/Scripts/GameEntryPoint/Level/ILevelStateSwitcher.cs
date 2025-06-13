public interface ILevelStateSwitcher
{
    void SwitchState<T>() where T : ILevelState;
}