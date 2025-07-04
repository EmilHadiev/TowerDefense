using UnityEngine;
using Zenject;

public class SniperGun : Gun
{
    [SerializeField] private ParticleView _criticalStrikeView;

    private const int CriticalChance = 20;

    private IGunPlace _gunPlace;

    private void OnEnable()
    {
        _criticalStrikeView.Stop();
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _gunPlace = player.GunPlace;
    }

    public override void HandleAttack(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            float criticalDamage = GetCriticalStrike();
            health.TakeDamage(criticalDamage);

            if (criticalDamage > 0)
            {
                Transform enemy = collider.transform;
                _criticalStrikeView.transform.position = enemy.transform.position + enemy.transform.up;
                _criticalStrikeView.Play();
            }
        }
    }

    private float GetCriticalStrike()
    {
        int randomValue = Random.Range(0, 100);
        return CriticalChance >= randomValue ? _gunPlace.CurrentGun.Damage * CriticalChance + CriticalChance : 0;
    }
}