using UnityEngine;

public interface IGun
{
    public float AttackSpeed { get; }
    public float Damage { get; }

    public void HandleAttack(Collider collider);
    public void SetData(GunData gunData);
}