using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;

    private IStateSwitcher _machine;

    public IMover Mover => _mover.Mover;

    private void OnValidate()
    {
        _mover ??= GetComponent<EnemyMover>();
    }

    private void Awake()
    {
        _machine = new EnemyStateMachine(Mover);
    }

    private void Start()
    {
        _machine.SwitchTo<EnemyMoveState>();
    }
}