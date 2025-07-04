using UnityEngine;

public class SplashBulletEffect : IBulletEffectHandler
{
    private const int Radius = 5;
    private const int MaxEnemies = 5;
    private const string EnemyMask = "Enemy";

    private readonly LayerMask _enemyMask;
    private readonly Collider[] _hits;
    private readonly Transform _bullet;
    private readonly PlayerStat _playerStat;

    public SplashBulletEffect(Transform bullet, PlayerStat playerStat)
    {
        _bullet = bullet;

        _enemyMask = LayerMask.GetMask(EnemyMask);
        _hits = new Collider[MaxEnemies];
        _playerStat = playerStat;
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
                health.TakeDamage(GetDamage() / enemiesCount);
    }

    private float GetDamage()
    {
        return _playerStat.Damage;
    }
}