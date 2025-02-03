using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyDieChecker))]
public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyType Type { get; private set; }
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private CharacterAnimator _animator;

    public IStateSwitcher StateMachine { get; private set; }
    public EnemyStat Stat { get; private set; }

    private void OnValidate()
    {
        _mover ??= GetComponent<EnemyMover>();
        _animator ??= GetComponent<CharacterAnimator>();
    }

    private void Start()
    {
        StateMachine = new EnemyStateMachine(_mover.Mover, _animator);
        StateMachine.SwitchTo<EnemyMoveState>();
    }

    [Inject]
    private void Constructor(IEnumerable<EnemyStat> stats)
    {
        foreach (var stat in stats)
        {
            if (stat.EnemyType == Type)
            {
                Stat = stat;
                return;
            }
        }

        throw new ArgumentNullException(nameof(stats));
    }
}