using UnityEngine;

class VampirismEffect : IBulletEffectHandler
{
    private readonly IHealth _playerHealth;
    private readonly PlayerStat _playerStat;

    public VampirismEffect(IHealth playerHealth, PlayerStat playerStat)
    {
        _playerHealth = playerHealth;
        _playerStat = playerStat;
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

    private float GetHalfDamage() => _playerStat.Damage / 2;
}
