using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.LanguageLegacy;

[RequireComponent(typeof(LanguageYG))]
public class UpgradeViewRender : MonoBehaviour
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _costText;

    private IUpgrader _upgrader;

    public void Initialize(IUpgrader data)
    {
        _upgrader = data;

        ShowData(_upgrader.Data);
        Translate();
    }

    public void UpdatePrice() => 
        _costText.text = _upgrader.Data.Cost.ToString();

    public void UpdateDescription() => 
        _upgradeDescriptionText.text = _upgrader.GetUpgradeDescription();

    private void ShowData(UpgradeData data)
    {
        _upgradeImage.sprite = data.Sprite;
        _upgradeNameText.text = data.Name;
    }

    private void Translate()
    {
        LocalizedText localizedText = _upgrader.Data.GetLocalizedText(YG2.lang);
        _upgradeNameText.text = localizedText.Name;
    }
}