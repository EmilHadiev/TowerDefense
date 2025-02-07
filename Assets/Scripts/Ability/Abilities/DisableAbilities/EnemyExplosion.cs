using UnityEngine;

public class EnemyExplosion : IDisableAbility
{
    private const int ExplosionRadius = 5;
    private const string PlayerMask = "Player";
    private const string EnemyMask = "Enemy";

    private readonly Transform _enemy;

    private Collider[] _hits = new Collider[10];    

    public EnemyExplosion(Transform enemy)
    {
        _enemy = enemy;
    }

    public void Activate()
    {
        int countEnemies = Physics.OverlapSphereNonAlloc(_enemy.position, ExplosionRadius, _hits, LayerMask.GetMask(PlayerMask, EnemyMask));

        Debug.Log(countEnemies);

        if (countEnemies > 0)
            AttackTargets(countEnemies);

        PhysicsDebug.DrawDebug(_enemy.position, ExplosionRadius, 1);
    }

    public void Deactivate() { }

    private void AttackTargets(int countEnemies)
    {
        for (int i = 0; i < countEnemies; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(25);
    }
}