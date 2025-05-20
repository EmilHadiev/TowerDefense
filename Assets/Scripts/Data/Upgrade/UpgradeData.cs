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

    [SerializeField] private LocalizedText[] _localizedTexts;

    public LocalizedText GetLocalizedText(string lang)
    {
        foreach (var localize in _localizedTexts)
            if (localize.Language.ToString().ToLower() == lang.ToLower())
                return localize;

        throw new ArgumentException(nameof(lang));
    }
}