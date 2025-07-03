using UnityEngine;

public interface IBulletDescription : ILootable
{
    string Description { get; }
    string FullDescription { get; }
    string Name { get; }
    Sprite Sprite { get; }
    LocalizedText GetLocalizedText(LanguageType languageType = LanguageType.none, string language = "none");
}