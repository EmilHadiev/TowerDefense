using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour, ILevelSwitcher
{
    [SerializeField] private PlayerUIDynamic _uiDynamic;
    [SerializeField] private EnemySpawnerContainer _spawnerContainer;
    [SerializeField] private WaitingLevelState _waitingState;

    private readonly Dictionary<Type, ILevelState> _states = new Dictionary<Type, ILevelState>(10);

    private EnemyUpgrader _upgrader;

    private ILevelState _currentState;

    private void Awake()
    {
        _states.Add(typeof(EnemySpawnerContainer), _spawnerContainer);
        _states.Add(typeof(WaitingLevelState), _waitingState);
        _states.Add(typeof(UpgradeLevelState), new UpgradeLevelState(_upgrader, this));
    }

    private void Start()
    {
        StopHideCanvas();

        StartEnemySpawn();
    }

    [Inject]
    private void Constructor(EnemyUpgrader upgrader)
    {
        _upgrader = upgrader;        
    }

    private void StopHideCanvas()
    {
        if (_uiDynamic.TryGetComponent(out Canvas canvas))
            if (canvas.enabled == false)
                canvas.enabled = true;
    }

    private void StartEnemySpawn() => SwitchTo<EnemySpawnerContainer>();

    public void SwitchTo<T>() where T : ILevelState
    {
        if (_states.TryGetValue(typeof(T), out ILevelState levelState) == false)
            new ArgumentOutOfRangeException(typeof(T).ToString());

        _currentState?.Exit();
        _currentState = levelState;
        _currentState.Enter();
    }
}