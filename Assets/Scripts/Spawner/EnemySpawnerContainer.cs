using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class EnemySpawnerContainer : MonoBehaviour,  ILevelState
{
    [SerializeField] private EnemySpawner[] _spawners;

    private const int SpawnDelay = (int)(Constants.EnemySpawnDelay * 1000);

    private SpawnLogic _spawnLogic;
    private SpawnLogicFactory _spawnLogicFactory;
    private LevelTracker _levelTracker;
    private EnemyCounter _counter;
    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;

    private CancellationTokenSource _spawnCts;

    private void OnEnable()
    {
        _counter.CapacityReached += OnFilled;
        _counter.AllEnemiesDead += OnEnemyDied;
    }

    private void OnDisable()
    {
        _counter.CapacityReached -= OnFilled;
        _counter.AllEnemiesDead -= OnEnemyDied;

        _spawnCts?.Cancel();
        _spawnCts?.Dispose();
    }

    [Inject]
    private void Constructor(EnemyCounter counter, ILevelStateSwitcher switcher, ITrainingMode trainingMode, WaveData waveData, LevelTracker levelTracker, ICoinStorage coinStorage)
    {
        _levelTracker = levelTracker;
        _counter = counter;
        _switcher = switcher;
        _trainingMode = trainingMode;

        _spawnLogicFactory = new SpawnLogicFactory(_levelTracker, waveData, _spawners, coinStorage);
        _spawnLogic = _spawnLogicFactory.Create();
    }

    public void Enter()
    {
        _spawnLogic.CalculateNextWave();
        _counter.Reset();
        Exit();

        _spawnCts = new CancellationTokenSource();
        SpawnEnemiesAsync(_spawnCts.Token).Forget();
    }

    public void Exit()
    {
        _spawnCts?.Cancel();
        _spawnCts?.Dispose();
        _spawnCts = null;
    }

    private async UniTaskVoid SpawnEnemiesAsync(CancellationToken ct)
    {
        try
        {
            while (ct.IsCancellationRequested == false)
            {
                await UniTask.Delay(SpawnDelay, cancellationToken: ct);

                if (ct.IsCancellationRequested) 
                    break;

                if (_spawnLogic.TrySpawn())
                {
                    _counter.Add();
                    await UniTask.Yield();
                }
            }
        }
        catch (Exception)
        {
            
        }
    }

    private void OnFilled() => Exit();

    private void OnEnemyDied()
    {
        if (_trainingMode.IsTrainingProcess())
            _trainingMode.ShowNextTraining();
        else
            _switcher.SwitchState<WaitingLevelState>();
    }

    private class SpawnLogicFactory
    {
        private readonly LevelTracker _levelTracker;
        private readonly WaveData _waveData;
        private readonly EnemySpawner[] _spawners;
        private readonly ICoinStorage _coinStorage;

        public SpawnLogicFactory(LevelTracker levelTracker, WaveData waveData, EnemySpawner[] spawners, ICoinStorage coinStorage)
        {
            _coinStorage = coinStorage;
            _levelTracker = levelTracker;
            _waveData = waveData;
            _spawners = spawners;
        }

        public SpawnLogic Create()
        {
            switch (_levelTracker.NumberLevelsCompleted)
            {
                case 0:
                    return new TrainingSpawnLogic(_waveData, _spawners, _coinStorage);
                default:
                    return new DefaultSpawnLogic(_waveData, _spawners);
            }
        }
    }
}