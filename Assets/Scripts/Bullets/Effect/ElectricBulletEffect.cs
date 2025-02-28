using UnityEngine;

public class ElectricBulletEffect : IBulletEffectHandler
{
    private const int Radius = 5;
    private const int MaxEnemies = 5;
    private const float DamagePercentage = 50;
    private const string EnemyMask = "Enemy";

    private readonly LayerMask _enemyMask;
    private readonly Collider[] _hits;
    private readonly Transform _bullet;
    private readonly BulletData _data;

    public ElectricBulletEffect(Transform bullet, BulletData data)
    {
        _bullet = bullet;
        _data = data;

        _enemyMask = LayerMask.GetMask(EnemyMask);
        _hits = new Collider[MaxEnemies];
    }

    public void HandleEffect(Collider enemy) => CastChainLightning();

    private void CastChainLightning()
    {
        int enemiesCount = Physics.OverlapSphereNonAlloc(_bullet.position, Radius, _hits, _enemyMask);

        if (enemiesCount == 0)
            return;

        PhysicsDebug.DrawDebug(_bullet.position, Radius);

        AttackTargets(enemiesCount);
    }

    private void AttackTargets(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(GetRadiusDamage());
    }

    private float GetRadiusDamage() => _data.Damage / 100 * DamagePercentage;
}