interface ILevelSwitcher
{
    void SwitchTo<T>() where T : ILevelState;
}