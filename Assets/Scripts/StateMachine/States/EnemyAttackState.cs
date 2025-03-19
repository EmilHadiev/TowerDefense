public class EnemyAttackState : IState
{
    private readonly IStateSwitcher _switcher;
    private readonly CharacterAnimator _animator;

    public EnemyAttackState(IStateSwitcher stateSwitcher, CharacterAnimator animator)
    {
        _switcher = stateSwitcher;
        _animator = animator;
    }

    public void Enter() => _animator.StartAttacking();

    public void Exit() => _animator.StopAttacking();
}