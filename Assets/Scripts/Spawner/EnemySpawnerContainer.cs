using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
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
    private void Constructor(EnemyCounter counter, ILevelStateSwitcher switcher, ITrainingMode trainingMode, WaveData waveData, LevelTracker levelTracker, ICoinStorage coinStorage, AwardGiver awardGiver)
    {
        _levelTracker = levelTracker;
        _counter = counter;
        _switcher = switcher;
        _trainingMode = trainingMode;

        _spawnLogicFactory = new SpawnLogicFactory(_levelTracker, waveData, _spawners, coinStorage, awardGiver);
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
        private const int AwardLevel = 4;

        private readonly LevelTracker _levelTracker;
        private readonly WaveData _waveData;
        private readonly EnemySpawner[] _spawners;
        private readonly ICoinStorage _coinStorage;
        private readonly AwardGiver _awardGiver;

        private readonly Dictionary<int, SpawnLogic> _spawnLogics = new Dictionary<int, SpawnLogic>();

        public SpawnLogicFactory(LevelTracker levelTracker, WaveData waveData, EnemySpawner[] spawners, ICoinStorage coinStorage, AwardGiver awardGiver)
        {
            _awardGiver = awardGiver;
            _coinStorage = coinStorage;
            _levelTracker = levelTracker;
            _waveData = waveData;
            _spawners = spawners;

            _spawnLogics.Add(0, new TrainingSpawnLogic(_waveData, _spawners, _coinStorage));
            _spawnLogics.Add(1, new FirstLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(2, new SecondLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(3, new ThirdLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(AwardLevel, new BossLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(-1, new DefaultSpawnLogic(_waveData, _spawners));
        }

        public SpawnLogic Create()
        {
            int currentLevel = _levelTracker.NumberLevelsCompleted;

            if (currentLevel >= _spawnLogics.Count - 1)
                return _spawnLogics[-1];

            return _spawnLogics[currentLevel];
        }

        private bool IsAwardLevel(int currentLevel)
        {
            if (_awardGiver.Bullets.TryGetValue(currentLevel, out IBulletDefinition data))
                return true;

            if (_awardGiver.Guns.TryGetValue(currentLevel, out GunData gunData))
                return true;

            return false;
        }
    }
}