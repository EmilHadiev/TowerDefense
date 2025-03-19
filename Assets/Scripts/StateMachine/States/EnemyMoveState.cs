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
        _animator.StartRunning();
        _mover.StartMove();
    }

    public void Exit()
    {
        _animator.StopRunning();
        _mover.StopMove();
    }
}