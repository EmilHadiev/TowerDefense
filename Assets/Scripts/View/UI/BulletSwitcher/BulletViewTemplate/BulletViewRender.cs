using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletViewRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _bulletIndexText;
    [SerializeField] private TMP_Text _bulletNameText;
    [SerializeField] private TMP_Text _bulletDescriptionText;
    [SerializeField] private TMP_Text _useText;
    [SerializeField] private TMP_Text _translatedDescription;
    [SerializeField] private Image _bulletImage;

    public string TranslatedDescription => _translatedDescription.text;

    private void Start() => HideTranslatedText();

    public void Render(IBulletDescription data, int index)
    {
        ShowData(data);
        _bulletIndexText.text = index.ToString();
    }

    public void UseTextEnableToggle(bool isOn)
    {
        _useText.gameObject.SetActive(isOn);
    }

    private void ShowData(IBulletDescription data)
    {
        _bulletImage.sprite = data.Sprite;
        _bulletDescriptionText.text = data.Description;
        _translatedDescription.text = data.FullDescription;
        _bulletNameText.text = data.Name;
    }

    private void HideTranslatedText() => 
        _translatedDescription.gameObject.SetActive(false);
}