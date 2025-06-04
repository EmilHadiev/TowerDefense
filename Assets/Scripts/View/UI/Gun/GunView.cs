using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class GunView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _gunImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _selectButton;

    private GunData _gunData;

    public event Action<Gun> Selected;
    public event Action<GunData> Purchased;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(Select);
        _purchaseButton.onClick.AddListener(Purchase);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(Select);
        _purchaseButton.onClick.RemoveListener(Purchase);
    }

    public void Initialize(GunData gunData)
    {
        _gunData = gunData;

        LocalizedText text = gunData.GetLocalizedText(YG2.lang);

        _gunImage.sprite = gunData.Sprite;
        _nameText.text = text.Name;
        _costText.text = gunData.Cost.ToString();
        ButtonsToggle();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BackgroundToggle(true);
    }

    private void Select()
    {
        Selected.Invoke(_gunData.Prefab);
        BackgroundToggle(true);
    }

    public void BackgroundToggle(bool isOn)
    {
        _backgroundImage.enabled = isOn;
    }

    private void Purchase()
    {
        Purchased.Invoke(_gunData);
    }

    private void ButtonsToggle()
    {
        if (_gunData.IsPurchased)
        {
            _purchaseButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }
        else
        {
            _purchaseButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
        }
    }
}
