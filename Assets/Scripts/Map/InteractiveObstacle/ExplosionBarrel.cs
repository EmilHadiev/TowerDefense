using UnityEngine;
using Zenject;

public class ExplosionBarrel : InteractiveObstacle
{
    private const int ExplosionRadius = 7;
    private const string EnemyMask = "Enemy";
    private const int DamageMultiplier = 2;

    private ICameraProvider _cameraProvider;
    private IFactoryParticle _factoryParticle;
    private IPlayerSoundContainer _soundContainer;
    private ParticleView _view;
    private IGunPlace _gunPlace;

    private readonly Collider[] _hits = new Collider[10];

    private void OnDisable()
    {
        ResetTargets();
    }

    private void Start()
    {
        CreateExplosionParticle();
    }

    [Inject]
    private void Constructor(IPlayer player, ICameraProvider cameraProvider, IFactoryParticle factoryParticle, IPlayerSoundContainer playerSoundContainer)
    {
        _gunPlace = player.GunPlace;
        _cameraProvider = cameraProvider;
        _factoryParticle = factoryParticle;
        _soundContainer = playerSoundContainer;
    }

    protected override void Activate(Collider collider)
    {
        PlayerView();
        Explode();
        PhysicsDebug.DrawDebug(transform.position, ExplosionRadius, 1);
        gameObject.SetActive(false);
    }

    private void PlayerView()
    {
        _cameraProvider.Punch();
        _view.Stop();
        _view.transform.position = transform.position;
        _view.Play();
        _soundContainer.Play(SoundName.FireExplosion);
    }

    private void CreateExplosionParticle()
    {
        _view = _factoryParticle.Create(AssetProvider.ParticleExplosionPath);
        _view.Stop();
    }

    private void Explode()
    {
        int countEnemies = Physics.OverlapSphereNonAlloc(transform.position, ExplosionRadius, _hits, LayerMask.GetMask(EnemyMask));

        if (countEnemies > 0)
            AttackTargets(countEnemies);

        AttackTargets(countEnemies);
    }

    private void AttackTargets(int countEnemies)
    {
        for (int i = 0; i < countEnemies; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(GetDamage());
    }

    private float GetDamage()
    {
        return _gunPlace.CurrentGun.Damage * DamageMultiplier;
    }

    private void ResetTargets()
    {
        for (int i = 0; i < _hits.Length; i++)
            if (_hits[i])
                _hits[i] = null;
    }
}