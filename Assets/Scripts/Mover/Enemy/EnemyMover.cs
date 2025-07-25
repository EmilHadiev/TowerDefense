using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;

    private IPlayer _player;
    private IMover _mover;
    private EnemyStat _stat;
    private Property _speedProperty;

    public IMover Mover => _mover;

    public Property Speed => _speedProperty;

    private void OnValidate()
    {
        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable() => _mover?.StopMove();

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;
        _speedProperty = new Property(_stat.Speed);
        _mover = new EnemyMoveToTargetPattern(_player, _agent, _speedProperty);
        SetMover(_mover);
        _mover.StopMove();
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0)
            _mover.Update();
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _player = player;
    }

    public void SetMover(IMover mover)
    {
        StopMove();
        _mover = mover;
        StartMove();
    }

    public void StartMove() => _mover?.StartMove();

    public void StopMove() => _mover?.StopMove();
}