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
    private EnemyCounter _counter;
    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;
    private int _index = 0;

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
    private void Constructor(EnemyCounter counter, ILevelStateSwitcher switcher, ITrainingMode trainingMode, WaveData waveData)
    {
        _counter = counter;
        _switcher = switcher;
        _trainingMode = trainingMode;
        _spawnLogic = new SpawnLogic(waveData, _spawners);
    }

    public void Enter()
    {
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
                SpawnEnemy();
            }
        }
        catch (Exception)
        {
            
        }
    }

    private void SpawnEnemy()
    {
        if (_index >= _spawners.Length)
            _index = 0;

        if (_spawners[_index].TrySpawn())
        {
            _index++;
            _counter.Add();
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
}