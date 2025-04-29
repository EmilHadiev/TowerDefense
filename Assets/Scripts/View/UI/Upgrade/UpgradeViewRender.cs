using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewRender : MonoBehaviour
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _costText;

    private IUpgrader _upgrader;

    public void Initialize(IUpgrader data) =>
        _upgrader = data;

    public void Show()
    {
        _upgradeImage.sprite = _upgrader.Data.Sprite;
        _upgradeNameText.text = _upgrader.Data.Name;
    }

    public void UpdatePrice()
    {
        _costText.text = _upgrader.Data.Cost.ToString();
    }

    public void UpdateDescription()
    {
        _upgradeDescriptionText.text = _upgrader.GetUpgradeDescription();
    }
}