using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _performer;

    public override void InstallBindings()
    {
        BindCoroutinePerformer();
        BindSceneSwitcher();
        BindSave();
        BindCoinStorage();
    }

    private void BindSave()
    {
        Container.BindInterfacesTo<YandexSaver>().AsSingle();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
    }

    private void BindSceneSwitcher()
    {
        Container.Bind<SceneSwitcher>().AsSingle();
    }

    private void BindCoroutinePerformer()
    {
        Container.BindInterfacesTo<CoroutinePerformer>().FromComponentInNewPrefab(_performer).AsSingle();
    }
}