using UnityEngine;

public class EnemyAttackState : IState
{
    private IStateSwitcher _switcher;

    public EnemyAttackState(IStateSwitcher stateSwitcher)
    {
        _switcher = stateSwitcher;
    }

    public void Enter()
    {
        Debug.Log($"Enter state: {nameof(EnemyAttackState)}");
    }

    public void Exit()
    {
        Debug.Log($"Exit state: {nameof(EnemyAttackState)}");
    }
}