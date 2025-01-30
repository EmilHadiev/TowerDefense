using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyType Type { get; private set; }
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private CharacterAnimator _animator;

    public IStateSwitcher StateMachine;

    public IMover Mover => _mover.Mover;

    private void OnValidate()
    {
        _mover ??= GetComponent<EnemyMover>();
        _animator = GetComponent<CharacterAnimator>();
    }

    private void Start()
    {
        StateMachine = new EnemyStateMachine(Mover, _animator);
        StateMachine.SwitchTo<EnemyMoveState>();
    }
}