using System;
using UnityEngine;

public class EnemyExplosion : IAbility
{
    private const int ExplosionRadius = 5;
    private const int DamageMultiplier = 2;
    private const string PlayerMask = "Player";
    private const string EnemyMask = "Enemy";

    private readonly Transform _enemy;
    private readonly EnemyStat _stat;
    private readonly ParticleViewText _text;

    private Collider[] _hits = new Collider[10];

    public event Action<int> Exploded;

    public EnemyExplosion(Transform enemy, EnemyStat stat, ParticleViewText text)
    {
        _enemy = enemy;
        _stat = stat;
        _text = text;
    }

    public void Activate()
    {
        int countEnemies = Physics.OverlapSphereNonAlloc(_enemy.position, ExplosionRadius, _hits, LayerMask.GetMask(PlayerMask, EnemyMask));

        if (countEnemies > 0)
            AttackTargets(countEnemies);

        _text.SetText(countEnemies.ToString());
        PhysicsDebug.DrawDebug(_enemy.position, ExplosionRadius, 1);
    }

    private void AttackTargets(int countEnemies)
    {
        for (int i = 0; i < countEnemies; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_stat.Damage * DamageMultiplier);
    }
}