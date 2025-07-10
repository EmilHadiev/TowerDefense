using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GunView : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _gunImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _descriptionButton;
    [SerializeField] private Button _selectButton;
    
    private ShopItemDescriptionContainer _descriptionContainer;
    private GunData _gunData;

    public event Action<Gun> Selected;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(Select);
        _descriptionButton.onClick.AddListener(ShowDescription);
        
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(Select);
        _descriptionButton.onClick.RemoveListener(ShowDescription);
    } 

    public void Initialize(GunData gunData, ShopItemDescriptionContainer shopItemDescriptionContainer)
    {
        _gunData = gunData;
        _descriptionContainer = shopItemDescriptionContainer;

        LocalizedText text = gunData.GetLocalizedText(YG2.lang);

        _gunImage.sprite = gunData.Sprite;
        _nameText.text = text.Name;
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

    private void ShowDescription()
    {
        LocalizedText text = _gunData.GetLocalizedText(YG2.lang);

        _descriptionContainer.SetDescription(text.FullDescription);
        _descriptionContainer.EnableToggle(true);
    }
}
