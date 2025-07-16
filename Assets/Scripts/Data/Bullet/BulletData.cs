using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet")]
public class BulletData : ScriptableObject, IBulletData, IBulletDescription
{
    [field: SerializeField, Range(1, 100f)] public float Speed { get; private set; }
    [field: SerializeField, Range(1, 5)] public int LifeTime { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public bool IsDropped { get; set; }


    [SerializeField] private LocalizedText[] _localizedTexts;

    public LocalizedText GetLocalizedText(LanguageType languageType, string language)
    {
        foreach (var localize in _localizedTexts)
            if (localize.Language == languageType || CompareByName(language, localize))
                return localize;

        throw new ArgumentException(nameof(languageType));
    }

    private bool CompareByName(string language, LocalizedText localized) =>
        localized.Language.ToString().ToLower() == language.ToLower();
}