using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpgradeViewRender : MonoBehaviour
{
    [SerializeField] private Image _gunImage;
    [SerializeField] private TMP_Text _gunNameText;
    [SerializeField] private TMP_Text _gunUpgradeDamageText;
    [SerializeField] private TMP_Text _gunUpgradeAttackSpeedText;

    private GunData _gunData;

    public void Initialize(GunData data)
    {
        _gunData = data;

        ShowData();
    }

    public void UpdateDescription()
    {
        _gunUpgradeDamageText.text = $"{_gunData.BaseDamage} > {_gunData.BaseDamage + _gunData.DamageUpgradeValue}";
        _gunUpgradeAttackSpeedText.text = $"{_gunData.AttackSpeedPercent} > {_gunData.AttackSpeedPercent + _gunData.AttackSpeedPercentageUpgradeValue}";
    }

    private void ShowData()
    {
        LocalizedText text = GetLocalizedText();

        _gunNameText.text = text.Name;

        _gunImage.sprite = _gunData.Sprite;
        UpdateDescription();
    }

    private LocalizedText GetLocalizedText() =>
        _gunData.GetLocalizedText(YG2.lang);
}