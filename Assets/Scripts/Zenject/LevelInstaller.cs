using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelEntryPoint _entryPoint;

    private void OnValidate()
    {
        _entryPoint ??= FindObjectOfType<LevelEntryPoint>();

        if (_entryPoint == null)
            Debug.LogError($"ERROR {nameof(_entryPoint)} is null!");
    }

    public override void InstallBindings()
    {
        BindLevelEntryPoint();
        BindEnemyCounter();
        BindCameraProvider();
    }

    private void BindCameraProvider()
    {
        Container.BindInterfacesTo<CameraProvider>().AsSingle();
    }

    private void BindEnemyCounter()
    {
        Container.Bind<EnemyCounter>().AsSingle();
    }

    private void BindLevelEntryPoint()
    {
        Container.BindInterfacesTo<LevelEntryPoint>().FromInstance(_entryPoint).AsSingle();
    }
}