using System;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractiveElement", fileName = "InteractiveElementData")]
public class InteractiveElementData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public InteractiveElement Prefab { get; private set; }

    [SerializeField] private LocalizedText[] _localizeTexts;

    public LocalizedText GetLocalizedText(string language)
    {
        foreach (var localizedText in _localizeTexts)
            if (language.Trim().ToLower() == localizedText.Language.ToString().Trim().ToLower())
                return localizedText;

        throw new ArgumentException(nameof(language));
    }
}