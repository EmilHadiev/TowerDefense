using UnityEngine;

public class AttackBarricade : Barricade
{
    [SerializeField] private ObstacleHealth _health;

    private const int AttackRadius = 5;
    private const string EnemyMask = "Enemy";

    private readonly Collider[] _hits = new Collider[Constants.MaxEnemies];

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void OnEnable()
    {
        _health.DamageApplied += Attack;
    }

    private void OnDisable()
    {
        _health.DamageApplied -= Attack;
    }

    private void Attack(float damage)
    {
        int countEnemies = Physics.OverlapSphereNonAlloc(transform.position, AttackRadius, _hits, LayerMask.GetMask(EnemyMask));

        if (countEnemies > 0)
            AttackTargets(countEnemies, GetDamage(damage, countEnemies));

        PhysicsDebug.DrawDebug(transform.position, AttackRadius, 1);
    }

    private float GetDamage(float damage, float countEnemies) =>
        damage / countEnemies;

    private void AttackTargets(int countEnemies, float damage)
    {
        for (int i = 0; i < countEnemies; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(damage);
    }
}