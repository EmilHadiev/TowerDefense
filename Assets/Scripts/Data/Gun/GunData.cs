using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunData")]
public class GunData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField, Range(0, 2)] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public int DamagePercent { get; private set; }
    [field: SerializeField] public Gun Prefab { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [SerializeField] private LocalizedText[] _texts;

    [SerializeField] public bool IsPurchased;

    public LocalizedText GetLocalizedText(string language)
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            if (_texts[i].Language.ToString().ToLower() == language.ToLower())
                return _texts[i];
        }

        throw new System.ArgumentException(nameof(language));
    }
}