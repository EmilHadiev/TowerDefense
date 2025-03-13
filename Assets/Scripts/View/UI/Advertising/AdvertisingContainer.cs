using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class AdvertisingContainer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _rewardValueText;

    protected IAdvertising Advertising;

    [Inject]
    private void Constructor(IAdvertising advertising)
    {
        Advertising = advertising;
    }

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    protected void SetText(string text) => _rewardValueText.text = text; 

    protected abstract void OnClick();
}