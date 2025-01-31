using UnityEngine;

public class EnemyMoveState : IState
{
    private readonly IStateSwitcher _switcher;
    private readonly IMover _mover;
    private readonly CharacterAnimator _animator;

    public EnemyMoveState(IStateSwitcher switcher, IMover mover, CharacterAnimator animator)
    {
        _switcher = switcher;
        _mover = mover;
        _animator = animator;
    }

    public void Enter()
    {
        Debug.Log($"Enter state: {nameof(EnemyMoveState)}");
        _animator.StartRunning();
        _mover.StartMove();
    }

    public void Exit()
    {
        Debug.Log($"Exit state: {nameof(EnemyMoveState)}");
        _animator.StopRunning();
        _mover.StopMove();
    }
}