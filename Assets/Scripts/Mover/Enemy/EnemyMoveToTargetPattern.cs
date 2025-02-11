using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToTargetPattern : IMover
{
    private readonly IPlayer _player;
    private readonly NavMeshAgent _agent;
    private readonly SpeedProperty _speedProperty;

    private bool _isWorking;

    public EnemyMoveToTargetPattern(IPlayer player, NavMeshAgent agent, SpeedProperty speedProperty)
    {
        _player = player;
        _agent = agent;
        _speedProperty = speedProperty;
        _agent.speed = _speedProperty.Speed;
    }

    public void StartMove()
    {
        _speedProperty.Changed += OnSpeedChanged;

        _isWorking = true;
        _agent.enabled = true;
    } 

    public void StopMove()
    {
        //_speedProperty.Changed -= OnSpeedChanged;

        _isWorking = false;
        _agent.enabled = false;
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        _agent.destination = _player.Transform.position;
        _agent.transform.LookAt(_player.Transform);
    }

    private void OnSpeedChanged(float speed)
    {
        _agent.speed = speed;
        Debug.Log($"current speed : {_agent.speed} or {speed}");
    }
}