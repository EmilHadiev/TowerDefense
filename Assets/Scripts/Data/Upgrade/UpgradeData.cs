using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField, Range(1, 100)] public float Value { get; set; }
    [field: SerializeField, Range(1, 100)] public int Cost { get; set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
}