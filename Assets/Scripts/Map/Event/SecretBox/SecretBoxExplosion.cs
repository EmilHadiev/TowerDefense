using UnityEngine;

public class SecretBoxExplosion : SecretBox
{
    private const int ExplosionRadius = 5;
    private const int DamageMultiplier = 2;
    private const string EnemyMask = "Enemy";

    private readonly Transform _transform;
    private readonly IGunPlace _gun;
    private readonly ICameraProvider _cameraProvider;
    private readonly ParticleView _view;

    private Collider[] _hits = new Collider[Constants.MaxEnemies];

    public SecretBoxExplosion(ISoundContainer soundContainer, Transform transform, 
        IGunPlace gunPlace, ICameraProvider cameraProvider, IFactoryParticle factoryParticle) : base(soundContainer)
    {
        _transform = transform;
        _gun = gunPlace;
        _cameraProvider = cameraProvider;

        _view = factoryParticle.Create(AssetProvider.ParticleExplosionPath);
        _view.transform.position = _transform.position;
        _view.Stop();
    }

    public override void Activate()
    {
        int countEnemies = Physics.OverlapSphereNonAlloc(_transform.position, ExplosionRadius, _hits, LayerMask.GetMask(EnemyMask));

        if (countEnemies > 0)
            AttackTargets(countEnemies);

        PlayExplosionView();
        ClearTargets();

        PhysicsDebug.DrawDebug(_transform.position, ExplosionRadius, 1);
    }

    private void PlayExplosionView()
    {
        _cameraProvider.Punch();
        PlaySound(SoundName.FireExplosion);
        _view.Play();
    }

    private void AttackTargets(int countEnemies)
    {
        for (int i = 0; i < countEnemies; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_gun.CurrentGun.Damage * DamageMultiplier);
    }

    private void ClearTargets()
    {
        for (int i = 0; i < _hits.Length; i++)
            _hits[i] = null;
    }
}