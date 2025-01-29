public interface IStateSwitcher
{
    void SwitchTo<TState>() where TState : IState;
}