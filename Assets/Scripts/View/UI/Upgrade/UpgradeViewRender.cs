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

    private const string EmojiMaxLevel = "<sprite=3>";

    private GunData _gunData;

    private bool _isDamageFilled;
    private bool _isAttackSpeedFilled;

    public void Initialize(GunData data)
    {
        _gunData = data;

        ShowData();
    }

    public void UpdateDescription()
    {
        if (_isDamageFilled == false)
            _gunUpgradeDamageText.text = $"{_gunData.BaseDamage} > {_gunData.BaseDamage + _gunData.DamageUpgradeValue}";

        if (_isAttackSpeedFilled == false)
            _gunUpgradeAttackSpeedText.text = $"{_gunData.AttackSpeedPercent} > {_gunData.AttackSpeedPercent + _gunData.AttackSpeedPercentageUpgradeValue}%";
    }

    public void ShowFilledDamage()
    {
        _isDamageFilled = true;
        _gunUpgradeDamageText.text = $"{EmojiMaxLevel}{_gunData.BaseDamage}{EmojiMaxLevel}";
    }

    public void ShowFilledAttackSpeed()
    {
        _isAttackSpeedFilled = true;
        _gunUpgradeAttackSpeedText.text = $"{EmojiMaxLevel}{_gunData.AttackSpeedPercent}%{EmojiMaxLevel}";
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