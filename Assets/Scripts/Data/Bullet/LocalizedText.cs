using System;
using UnityEngine;

[Serializable]
public struct LocalizedText
{
    public LocalizedText(LanguageType language, string name, string shortDescription, string fullDescription)
    {
        Language = language;
        Name = name;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
    }

    [field: SerializeField] public LanguageType Language { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string ShortDescription { get; private set; }
    [field: SerializeField, TextArea(3,3)] public string FullDescription { get; private set; }
}