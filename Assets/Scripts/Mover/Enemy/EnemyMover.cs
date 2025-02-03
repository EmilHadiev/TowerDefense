using System.Collections.Generic;
using System;
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

    public IMover Mover => _mover;

    public float Speed => _stat.Speed;

    private void OnValidate()
    {
        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;
        _mover = new EnemyMoveToTargetPattern(_player, _agent, this);
        SetMover(_mover);
        _mover.StopMove();
    }

    private void Update() => _mover.Update();

    [Inject]
    private void Constructor(IPlayer player)
    {
        _player = player;
    }

    public void SetMover(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }
}