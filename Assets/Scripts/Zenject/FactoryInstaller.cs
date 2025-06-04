using Zenject;

public class FactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindParticleFactory();
        BindEnemyFactory();
        BindInteractiveElementFactory();
        BindGunFactory();
    }

    private void BindEnemyFactory()
    {
        Container.BindInterfacesTo<EnemyFactory>().AsSingle();
    }

    private void BindParticleFactory()
    {
        Container.BindInterfacesTo<FactoryParticle>().AsSingle();
    }

    private void BindInteractiveElementFactory()
    {
        Container.BindInterfacesTo<InteractiveElementFactory>().AsSingle();
    }

    private void BindGunFactory()
    {
        Container.BindInterfacesAndSelfTo<GunFactory>().AsSingle();
    }
}