using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunData")]
public class GunData : ScriptableObject
{
    [field: SerializeField] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField] public int DamagePercent { get; private set; }
    [field: SerializeField] public Gun Gun { get; private set; }
}