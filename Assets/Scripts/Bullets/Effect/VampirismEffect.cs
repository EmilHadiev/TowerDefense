using UnityEngine;

class VampirismEffect : IBulletEffectHandler
{
    private const int DamageReducer = 2;

    private readonly IHealth _playerHealth;
    private readonly IGunPlace _gunPlace;

    public VampirismEffect(IHealth playerHealth, IGunPlace gunPlace)
    {
        _playerHealth = playerHealth;
        _gunPlace = gunPlace;
    }

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth enemyHealth))
        {
            int damage = (int)GetHalfDamage();

            _playerHealth.AddHealth(damage);
            enemyHealth.TakeDamage(damage);
        }
    }

    private float GetHalfDamage() => _gunPlace.CurrentGun.Damage / DamageReducer;
}
