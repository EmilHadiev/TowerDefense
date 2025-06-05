using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDescriptionContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _buttonClose;

    private void OnEnable() => _buttonClose.onClick.AddListener(OnClick);

    private void OnDisable() => _buttonClose.onClick.RemoveListener(OnClick);

    public void SetDescription(string description) => 
        _descriptionText.text = description;

    public void EnableToggle(bool isOn) => 
        gameObject.SetActive(isOn);

    private void OnClick() => EnableToggle(false);
}