using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _speed = 1f;

    private IPlayer _player;
    private IMover _mover;

    public float Speed => _speed;

    private void OnValidate()
    {
        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.speed = _speed;
        _mover = new EnemyMoveToTargetPattern(_player, _agent, this);
        SetMover(_mover);
    }

    private void Update() => _mover.Update();

    [Inject]
    private void Constructor(IPlayer player) => _player = player;

    public void SetMover(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }
}