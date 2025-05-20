using System;
using UnityEngine;

[Serializable]
public struct LocalizedText
{
    [field: SerializeField] public LanguageType Language { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string ShortDescription { get; private set; }
    [field: SerializeField, TextArea(2,2)] public string FullDescription { get; private set; }
}