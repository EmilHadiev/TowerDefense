using UnityEngine;

public class ExtraDamageBulletEffect : IBulletEffectHandler
{
    private const float AdditionalPercentageDamage = 50;
    private readonly BulletData _data;
    private readonly PlayerStat _playerStat;

    public ExtraDamageBulletEffect(BulletData data, PlayerStat playerStat)
    {
        _data = data;
        _playerStat = playerStat;
    }

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(GetAdditionalDamage());
    }

    private float GetAdditionalDamage() => GetDamage() / 100 * AdditionalPercentageDamage;

    private float GetDamage() => _data.Damage + _playerStat.Damage;
}