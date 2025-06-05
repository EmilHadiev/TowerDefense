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
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private TMP_Text _purchaseButtonText;
    [SerializeField] private Button _selectButton;
    
    private ShopItemDescriptionContainer _descriptionContainer;
    private GunData _gunData;
    private IPurchaser _puchaser;
    private IPlayerSoundContainer _playerSoundContainer;

    public event Action<Gun> Selected;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(Select);
        _purchaseButton.onClick.AddListener(Purchase);
        _descriptionButton.onClick.AddListener(ShowDescription);
        
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(Select);
        _purchaseButton.onClick.RemoveListener(Purchase);
        _descriptionButton.onClick.RemoveListener(ShowDescription);
    } 

    public void Initialize(GunData gunData, IPurchaser purchaser, IPlayerSoundContainer playerSoundContainer, ShopItemDescriptionContainer shopItemDescriptionContainer)
    {
        _gunData = gunData;
        _puchaser = purchaser;
        _playerSoundContainer = playerSoundContainer;
        _descriptionContainer = shopItemDescriptionContainer;

        LocalizedText text = gunData.GetLocalizedText(YG2.lang);

        _gunImage.sprite = gunData.Sprite;
        _nameText.text = text.Name;
        _purchaseButtonText.text = gunData.Price.ToString();
        ButtonsToggle();
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
        if (_puchaser.TryPurchase(_gunData, () => _playerSoundContainer.Play(SoundName.SwitchBullet)))
        {
            ButtonsToggle();
            Selected.Invoke(_gunData.Prefab);
            BackgroundToggle(true);
        }
    }

    private void ShowDescription()
    {
        LocalizedText text = _gunData.GetLocalizedText(YG2.lang);

        _descriptionContainer.SetDescription(text.FullDescription);
        _descriptionContainer.EnableToggle(true);
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
