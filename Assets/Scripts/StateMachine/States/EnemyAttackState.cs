public class EnemyAttackState : IState
{
    private readonly CharacterAnimator _animator;

    public EnemyAttackState(CharacterAnimator animator)
    {
        _animator = animator;
    }

    public void Enter() => _animator.StartAttacking();

    public void Exit() => _animator.StopAttacking();
}