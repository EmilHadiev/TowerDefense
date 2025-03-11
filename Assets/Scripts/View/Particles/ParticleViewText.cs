using CartoonFX;
using UnityEngine;

public class ParticleViewText : ParticleView
{
    [SerializeField] private CFXR_ParticleText _text;

    public void SetText(string text) => _text.UpdateText(text);

    public new void SetColor(Color color) => _text.SetTextColor(color);
}