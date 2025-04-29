using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletViewRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _bulletIndexText;
    [SerializeField] private TMP_Text _bulletNameText;
    [SerializeField] private TMP_Text _bulletDescriptionText;
    [SerializeField] private TMP_Text _useText;
    [SerializeField] private Image _bulletImage;

    public void Render(IBulletDescription data, int index)
    {
        _bulletImage.sprite = data.Sprite;
        _bulletDescriptionText.text = data.Description;
        _bulletNameText.text = data.Name;
        _bulletIndexText.text = index.ToString();
    }

    public void UseTextEnableToggle(bool isOn)
    {
        _useText.gameObject.SetActive(isOn);
    }
}