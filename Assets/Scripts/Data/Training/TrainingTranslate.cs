using System;
using UnityEngine;

[Serializable]
public struct TrainingTranslate
{
    [TextArea(2, 2), SerializeField] private string _ru;
    [TextArea(2, 2), SerializeField] private string _en;
    [TextArea(2, 2), SerializeField] private string _tr;

    public string GetTranslate(string lang)
    {
        switch (lang)
        {
            case "ru":
                return _ru;
            case "en":
                return _en;
            case "tr":
                return _tr;
            default:
                throw new ArgumentException(nameof(lang));
        }
    }
}