using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelStateMachine _entryPoint;
    [SerializeField] private TrainingMode _trainingMode;
    [SerializeField] private WaveData _waveData;

    private void OnValidate()
    {
        _entryPoint ??= FindObjectOfType<LevelStateMachine>();
        _trainingMode ??= FindObjectOfType<TrainingMode>();

        if (_entryPoint == null)
            Debug.LogError($"ERROR {nameof(_entryPoint)} is null!");
    }

    public override void InstallBindings()
    {
        BindLevelEntryPoint();
        BindEnemyCounter();
        BindGameOverService();
        BindTrainingMode();
        BindWaveData();
        BindWaveCounter();
    }

    private void BindWaveCounter()
    {
        Container.BindInterfacesAndSelfTo<WaveCounter>().AsSingle();
    }

    private void BindGameOverService()
    {
        Container.BindInterfacesTo<GameOverService>().AsSingle();
    }

    private void BindEnemyCounter()
    {
        Container.Bind<EnemyCounter>().AsSingle();
    }

    private void BindLevelEntryPoint()
    {
        Container.BindInterfacesTo<LevelStateMachine>().FromInstance(_entryPoint).AsSingle();
    }

    private void BindTrainingMode()
    {
        Container.BindInterfacesTo<TrainingMode>().FromInstance(_trainingMode).AsSingle();
    }

    private void BindWaveData()
    {
        Container.Bind<WaveData>().FromNewScriptableObject(_waveData).AsSingle();
    }
}