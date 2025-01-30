using UnityEngine;

public class EnemyAttackState : IState
{
    private readonly IStateSwitcher _switcher;
    private readonly CharacterAnimator _animator;

    public EnemyAttackState(IStateSwitcher stateSwitcher, CharacterAnimator animator)
    {
        _switcher = stateSwitcher;
        _animator = animator;
    }

    public void Enter()
    {
        Debug.Log($"Enter state: {nameof(EnemyAttackState)}");
        _animator.StartAttacking();
    }

    public void Exit()
    {
        Debug.Log($"Exit state: {nameof(EnemyAttackState)}");
        _animator.StopAttacking();
    }
}