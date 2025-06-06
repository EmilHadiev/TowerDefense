using UnityEngine;

public interface IGun
{
    public float DamagePercent { get; }
    public float BaseAttackSpeed { get; }

    public void HandleAttack(Collider collider);
    public void SetData(GunData gunData);
}