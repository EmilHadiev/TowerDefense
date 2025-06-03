using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunData")]
public class GunData : ScriptableObject
{
    [field: SerializeField, Range(0, 2)] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public int DamagePercent { get; private set; }
    [field: SerializeField] public Gun Prefab { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [SerializeField] public bool IsPurchased;
}