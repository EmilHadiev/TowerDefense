using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletSwitcherView : MonoBehaviour, IBulletSwitcherView
{
    [SerializeField] private TMP_Text _bulletIndexText;
    [SerializeField] private Image _bulletImage;
    [SerializeField] private TMP_Text _bulletNameText;
    [SerializeField] private TMP_Text _bulletDescriptionText;
    [SerializeField] private TMP_Text _useText;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _showDescriptionButton;
    [SerializeField] private PurchaseBulletContainer _purchaseContainer;

    private IBulletDescription _data;
    private IBulletPurchaseHandler _purchaseHandler;

    private int _index;

    public event Action<int> Used;
    public event Action<string> Clicked;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUsed);
        _showDescriptionButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUsed);
        _showDescriptionButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(IBulletDescription bulletData, int index, IBulletPurchaseHandler bulletPurchaseHander)
    {
        _data = bulletData;
        _index = index;

        if (_purchaseHandler != null)
            _purchaseHandler.Purchased -= OnPurchased;

        _purchaseHandler = bulletPurchaseHander;
        _purchaseHandler.Purchased += OnPurchased;

        ShowData();
    }

    private void OnDestroy()
    {
        _purchaseHandler.Purchased -= OnPurchased;
    }

    private void ShowData()
    {
        _bulletImage.sprite = _data.Sprite;
        _bulletDescriptionText.text = _data.Description;
        _bulletNameText.text = _data.Name;
        _bulletIndexText.text = _index.ToString();
        _purchaseContainer.SetText(_data.Price);
        TogglePurchaseContainer(_data.IsPurchased);
    }

    private void OnUsed()
    {
        if (_data.IsPurchased == false)
        {
            if (_purchaseHandler.TryPurchase(_data))
                Used?.Invoke(_index);
        }
        else
        {
            Used?.Invoke(_index);
        }
    }

    private void OnClicked() => Clicked?.Invoke(_data.FullDescription);

    private void OnPurchased(IBulletDescription data)
    {
        if (data == _data)
            TogglePurchaseContainer(true);
    }

    private void TogglePurchaseContainer(bool isPurchased)
    {
        if (isPurchased)
        {
            _useText.gameObject.SetActive(true);
            _purchaseContainer.gameObject.SetActive(false);
        }
        else
        {
            _useText.gameObject.SetActive(false);
            _purchaseContainer.gameObject.SetActive(true);
        }
    }
}