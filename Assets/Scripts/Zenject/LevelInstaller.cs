using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelStateMachine _entryPoint;
    [SerializeField] private TrainingMode _trainingMode;
    [SerializeField] private WaveData _waveData;
    [SerializeField] private CameraProvider _camera;

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
        BindComboSystem();
        BindGameOverService();
        BindTrainingMode();
        BindWaveData();
        BindWaveCounter();
        BindCameraProvider();
        BindLeaderBoards();
    }

    private void BindLeaderBoards()
    {
        Container.BindInterfacesAndSelfTo<YGLeaderBoardService>().AsSingle();
    }

    private void BindComboSystem()
    {
        Container.BindInterfacesTo<ComboSystem>().AsSingle();
    }

    private void BindCameraProvider()
    {
        Container.BindInterfacesTo<CameraProvider>().FromInstance(_camera).AsSingle();
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