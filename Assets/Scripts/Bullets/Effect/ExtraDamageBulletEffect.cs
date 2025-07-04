using UnityEngine;

public class ExtraDamageBulletEffect : IBulletEffectHandler
{
    private const float AdditionalPercentageDamage = 50;
    private readonly PlayerStat _playerStat;
    private readonly IGunPlace _gunPlace;

    public ExtraDamageBulletEffect(IGunPlace gunPlace, PlayerStat playerStat)
    {
        _gunPlace = gunPlace;
        _playerStat = playerStat;
    }

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(GetAdditionalDamage());
    }

    private float GetAdditionalDamage() => GetDamage() / 100 * AdditionalPercentageDamage;

    private float GetDamage() => _gunPlace.CurrentGun.Damage + _playerStat.Damage;
}