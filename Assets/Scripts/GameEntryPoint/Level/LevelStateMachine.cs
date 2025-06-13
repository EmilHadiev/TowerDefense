using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelStateMachine : MonoBehaviour, ILevelStateSwitcher
{
    [Header("Level state")]
    [SerializeField] private PlayerUIDynamic _uiDynamic;
    [SerializeField] private EnemySpawnerContainer _spawnerContainer;
    [SerializeField] private WaitingLevelState _waitingState;
    [Header("Player")]
    [SerializeField] private PlayerSpawnPosition _spawnPosition;
    [SerializeField] private GunViewContainer _gunContainer;
    [Header("Map")]
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private Map _map;
    

    private ITrainingMode _trainingMode;

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
        StartLevel();

        if (_trainingMode.IsTrainingProcess() == false)
        {
            _trainingMode.TrainingOver();
            StartEnemySpawn();
        }
    }

    private void StartLevel()
    {
        _loadingScreen.Show();
        StopHideCanvas();
        SetPlayerPosition();
        SetGunToPlayer();

        CreateMap();
        BuildNavMesh();
        _loadingScreen.Hide();
    }

    private void StartTraining()
    {
        _trainingMode.InitTraining(this);
        _trainingMode.ShowNextTraining();
    }

    [Inject]
    private void Constructor(EnemyUpgrader upgrader, LoadingScreen loadingScreen, IPlayer player, ITrainingMode trainingMode)
    {
        _upgrader = upgrader;
        _loadingScreen = loadingScreen;
        _player = player;
        _trainingMode = trainingMode;
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

    private void StartEnemySpawn() => SwitchState<EnemySpawnerContainer>();

    private void SetPlayerPosition() => _player.Transform.position = _spawnPosition.transform.position;

    private void SetGunToPlayer() => _player.GunPlace.SetGun(_gunContainer.CreateAndSetAvailableGun());

    private void CreateMap()
    {
        Map map = Instantiate(_map);
    }

    private void BuildNavMesh() => _navMeshSurface.BuildNavMesh();

    private void OnLoaded()
    {
        ActivatePlatformOptions();

        if (_trainingMode.IsTrainingProcess())
            StartTraining();
    }
}