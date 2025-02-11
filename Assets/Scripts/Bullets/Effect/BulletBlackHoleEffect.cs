using DG.Tweening;
using UnityEngine;

public class BulletBlackHoleEffect : IBulletEffectHandler
{
    private const float Radius = 5f;
    private const float AttractTime = 5f;
    private const int MaxEnemies = 10;
    private const string EnemyMask = "Enemy";

    private readonly LayerMask _layerMask;
    private readonly Collider[] _hits = new Collider[MaxEnemies];
    private readonly Transform _bullet;

    public BulletBlackHoleEffect(Transform bullet)
    {
        _layerMask = LayerMask.GetMask(EnemyMask);
        _bullet = bullet;
    }

    public void HandleEffect(Collider enemy)
    {
        AttractTo(enemy.transform);
    }

    private void AttractTo(Transform enemy)
    {
        int enemiesCount = Physics.OverlapSphereNonAlloc(_bullet.position, Radius, _hits, _layerMask);

        if (enemiesCount == 0)
            return;

        for (int i = 0; i < enemiesCount; i++)
            if (_hits[i].TryGetComponent(out Transform target))
                target.DOMove(_bullet.position, AttractTime);
    }
}