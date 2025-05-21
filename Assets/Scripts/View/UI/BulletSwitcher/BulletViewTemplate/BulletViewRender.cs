using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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
        ViewShow(data, index);
        Translate(data);
    }

    public void UseTextEnableToggle(bool isOn) =>
        _useText.gameObject.SetActive(isOn);

    private void ViewShow(IBulletDescription data, int index)
    {
        _bulletImage.sprite = data.Sprite;
        _bulletIndexText.text = index.ToString();
    }

    private void Translate(IBulletDescription data)
    {
        LocalizedText localizedText = data.GetLocalizedText(language: YG2.lang);

        _bulletDescriptionText.text = localizedText.ShortDescription;
        _translatedDescription.text = localizedText.FullDescription;
        _bulletNameText.text = localizedText.Name;
    }

    private void HideTranslatedText() => 
        _translatedDescription.gameObject.SetActive(false);
}