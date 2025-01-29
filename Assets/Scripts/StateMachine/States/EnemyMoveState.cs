using UnityEngine;

public class EnemyMoveState : IState
{
    private IStateSwitcher _switcher;

    public void Enter()
    {
        Debug.Log($"Enter state: {nameof(EnemyAttackState)}");
    }

    public void Exit()
    {
        Debug.Log($"Exit state: {nameof(EnemyAttackState)}");
    }
}