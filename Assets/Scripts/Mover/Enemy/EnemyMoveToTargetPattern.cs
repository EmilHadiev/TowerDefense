using UnityEngine.AI;

public class EnemyMoveToTargetPattern : IMover
{
    private IPlayer _player;
    private NavMeshAgent _agent;
    private IMovable _movable;

    private bool _isWorking;

    public EnemyMoveToTargetPattern(IPlayer player, NavMeshAgent agent, IMovable movable)
    {
        _player = player;
        _agent = agent;
        _movable = movable;
        _agent.speed = _movable.Speed;
    }

    public void StartMove() => _isWorking = true;

    public void StopMove() => _isWorking = false;

    public void Update()
    {
        if (_isWorking == false)
            return;

        MoveToPlayer();
    }

    private void MoveToPlayer() => _agent.destination = _player.Transform.position;
}