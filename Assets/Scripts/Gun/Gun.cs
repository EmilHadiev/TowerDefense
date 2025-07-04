using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    private GunData _gunData;

    public float AttackSpeed => _gunData.GetTotalAttackSpeed();
    public float Damage => _gunData.GetTotalDamage();

    public void SetData(GunData gunData)
    {
        _gunData = gunData;
    }

    public virtual void HandleAttack(Collider collider)
    {
        
    }
}