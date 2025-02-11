using UnityEngine;
using Zenject;

public class CoroutineInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _performer;

    public override void InstallBindings()
    {
        BindCoroutinePerformer();
    }

    private void BindCoroutinePerformer()
    {
        Container.BindInterfacesTo<CoroutinePerformer>().FromComponentInNewPrefab(_performer).AsSingle().NonLazy();
    }
}