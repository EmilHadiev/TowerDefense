using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelStateMachine : MonoBehaviour, ILevelStateSwitcher
{
    [SerializeField] private PlayerUIDynamic _uiDynamic;
    [SerializeField] private EnemySpawnerContainer _spawnerContainer;
    [SerializeField] private WaitingLevelState _waitingState;
    [SerializeField] private PlayerSpawnPosition _spawnPosition;

    private readonly Dictionary<Type, ILevelState> _states = new Dictionary<Type, ILevelState>(10);

    private EnemyUpgrader _upgrader;
    private LoadingScreen _loadingScreen;
    private IPlayer _player;

    private ILevelState _currentState;

    private void Awake()
    {
        _states.Add(typeof(EnemySpawnerContainer), _spawnerContainer);
        _states.Add(typeof(WaitingLevelState), _waitingState);
        _states.Add(typeof(EnemyUpgradeState), new EnemyUpgradeState(_upgrader, this));
    }

    private void OnEnable()
    {
        _loadingScreen.Loaded += OnLoaded;
    }

    private void OnDisable()
    {
        _loadingScreen.Loaded -= OnLoaded;
    }

    private void Start()
    {
        StopHideCanvas();        
        StartEnemySpawn();
        SetPlayerPosition();
        _loadingScreen.Show();
    }

    [Inject]
    private void Constructor(EnemyUpgrader upgrader, LoadingScreen loadingScreen, IPlayer player)
    {
        _upgrader = upgrader;
        _loadingScreen = loadingScreen;
        _player = player;
    }

    #if UNITY_WEBGL

    private GameplayMarkup _markup;

    [Inject]
    private void WebConstructor(GameplayMarkup markup)
    {
        _markup = markup;
    }

    private void StartGameplay() => _markup.Start();
    #endif

    private void ActivatePlatformOptions()
    {
        #if UNITY_WEBGL
            StartGameplay();
        #endif
    }

    public void SwitchState<T>() where T : ILevelState
    {
        if (_states.TryGetValue(typeof(T), out ILevelState levelState) == false)
            new ArgumentOutOfRangeException(typeof(T).ToString());

        _currentState?.Exit();
        _currentState = levelState;
        _currentState.Enter();
    }

    private void StopHideCanvas()
    {
        if (_uiDynamic.TryGetComponent(out Canvas canvas))
            if (canvas.enabled == false)
                canvas.enabled = true;
    }

    private void StartEnemySpawn() => SwitchState<EnemyUpgradeState>();

    private void SetPlayerPosition() => _player.Transform.position = _spawnPosition.transform.position;

    private void OnLoaded()
    {
        ActivatePlatformOptions();
    }
}