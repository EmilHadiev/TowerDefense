using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    public float DamagePercent { get; private set; }

    public float BaseAttackSpeed { get; private set; }

    public void SetData(GunData gunData)
    {
        DamagePercent = gunData.DamagePercent;
        BaseAttackSpeed = gunData.BaseAttackSpeed;
    }

    public virtual void HandleAttack(Collider collider)
    {
        
    }
}