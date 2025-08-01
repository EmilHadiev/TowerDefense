using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class EnemySpawnerContainer : MonoBehaviour,  ILevelState
{
    [SerializeField] private MapEventsContainer _eventsContainer;
    [SerializeField] private EnemySpawner[] _spawners;

    private const int SpawnDelay = (int)(Constants.EnemySpawnDelay * 1000);

    private SpawnLogic _spawnLogic;
    private SpawnLogicFactory _spawnLogicFactory;
    private LevelTracker _levelTracker;
    private EnemyCounter _counter;
    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;

    private CancellationTokenSource _spawnCts;

    private bool _isEventActivated;

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

        TryActivateEvent();

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

    private void TryActivateEvent()
    {
        if (_spawnLogicFactory.IsNotDefaultLevel() && _isEventActivated == false)
        {
            _eventsContainer.TryActivateEvent();
            _isEventActivated = true;
        }
        else
        {
            _eventsContainer.Deactivate();
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
        private const int AwardLevel = -2;
        private const int TrainingLevel = 0;
        private const int DefaultLevel = -1;
        private const int AdditionalCoins = 75;

        private readonly LevelTracker _levelTracker;
        private readonly WaveData _waveData;
        private readonly EnemySpawner[] _spawners;
        private readonly ICoinStorage _coinStorage;
        private readonly AwardGiver _awardGiver;

        private readonly Dictionary<int, SpawnLogic> _spawnLogics = new Dictionary<int, SpawnLogic>();

        public SpawnLogicFactory(LevelTracker levelTracker, WaveData waveData, EnemySpawner[] spawners, ICoinStorage coinStorage, AwardGiver awardGiver)
        {
            _coinStorage = coinStorage;
            _levelTracker = levelTracker;
            _waveData = waveData;
            _spawners = spawners; 
            _awardGiver = awardGiver;

            _spawnLogics.Add(TrainingLevel, new TrainingSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(1, new FirstLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(2, new SecondLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(3, new ThirdLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(AwardLevel, new BossLevelSpawnLogic(_waveData, _spawners));
            _spawnLogics.Add(DefaultLevel, new DefaultSpawnLogic(_waveData, _spawners));            
        }

        public SpawnLogic Create()
        {
            int currentLevel = _levelTracker.NumberLevelsCompleted;

            if (IsTrainingLevel(currentLevel))
            {
                Debug.Log("Тренировочный уровень");
                TryAddAdditionalCoins();
                currentLevel = TrainingLevel;
            }
            else if (IsRewardLevel())
            {
                Debug.Log("Уровень с наградой");
                currentLevel = AwardLevel;
            }
            else if (IsNotDefaultLevel())
            {
                currentLevel = DefaultLevel;
                Debug.Log("Стандартный уровень");
            }
            else
            {
                Debug.Log("Ничего не меняю!");
            }

            return _spawnLogics[currentLevel];
        }

        private void TryAddAdditionalCoins()
        {
            if (_levelTracker.NumberLevelsCompleted > 0 || _coinStorage.Coins > 0)
                return;

            _coinStorage.Add(AdditionalCoins);
        }

        private bool IsTrainingLevel(int currentLevel) => currentLevel == TrainingLevel;

        private bool IsRewardLevel()
        {
            return _awardGiver.IsRewardLevel();
        }

        public bool IsNotDefaultLevel()
        {
            int currentLevel = _levelTracker.NumberLevelsCompleted;
            return _spawnLogics.TryGetValue(currentLevel, out SpawnLogic value) == false;
        }
    }
}