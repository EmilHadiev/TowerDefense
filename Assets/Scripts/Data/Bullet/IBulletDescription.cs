using UnityEngine;

public interface IBulletDescription : ILootable
{
    Sprite Sprite { get; }
    LocalizedText GetLocalizedText(LanguageType languageType = LanguageType.none, string language = "none");
}