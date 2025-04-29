using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BulletViewRender))]
public class BulletView : MonoBehaviour, IBulletView
{
    [SerializeField] private BulletViewRender _render;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _showDescriptionButton;
    [SerializeField] private PurchaseBulletContainer _purchaseContainer;

    private IBulletDescription _data;
    private IBulletPurchaseHandler _purchaseHandler;

    private int _index;

    public event Action<int> Used;
    public event Action<string> Clicked;

    private void OnValidate()
    {
        _render ??= GetComponent<BulletViewRender>();
    }

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

        _purchaseHandler = bulletPurchaseHander;

        ShowData();
    }

    private void ShowData()
    {
        _render.Render(_data, _index);
        _purchaseContainer.SetText(_data.Price);
        TogglePurchaseContainer(_data.IsPurchased);
    }

    private void OnUsed()
    {
        if (_data.IsPurchased == false)
        {
            if (_purchaseHandler.TryPurchase(_data))
            {
                TogglePurchaseContainer(true);
                Used?.Invoke(_index);
            }
        }
        else
        {
            Used?.Invoke(_index);
        }
    }

    private void OnClicked() => Clicked?.Invoke(_data.FullDescription);

    private void TogglePurchaseContainer(bool isPurchased)
    {
        if (isPurchased)
        {
            _render.UseTextEnableToggle(true);
            _purchaseContainer.gameObject.SetActive(false);
        }
        else
        {
            _render.UseTextEnableToggle(false);
            _purchaseContainer.gameObject.SetActive(true);
        }
    }
}