using TMPro;
using UnityEngine.UI;

public class BulletSwitcherRender : IBulletSwitcherRender
{
    private readonly TMP_Text _bulletIndexText;
    private readonly TMP_Text _bulletNameText;
    private readonly TMP_Text _bulletDescriptionText;
    private readonly Image _bulletImage;

    public BulletSwitcherRender(TMP_Text bulletIndexText, TMP_Text bulletNameText, TMP_Text bulletDescriptionText, Image bulletImage)
    {
        _bulletIndexText = bulletIndexText;
        _bulletNameText = bulletNameText;
        _bulletDescriptionText = bulletDescriptionText;
        _bulletImage = bulletImage;
    }

    public void Render(IBulletDescription data, int index)
    {
        _bulletImage.sprite = data.Sprite;
        _bulletDescriptionText.text = data.Description;
        _bulletNameText.text = data.Name;
        _bulletIndexText.text = index.ToString();
    }
}