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
    [SerializeField] private BuyBulletContainer _buyContainer;

    private IBulletDescription _data;
    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;

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

    public void Initialize(IBulletDescription bulletData, int index, ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _data = bulletData;
        _index = index;
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        ShowData();
    }

    private void ShowData()
    {
        _bulletImage.sprite = _data.Sprite;
        _bulletDescriptionText.text = _data.Description;
        _bulletNameText.text = _data.Name;
        _bulletIndexText.text = _index.ToString();
        _buyContainer.SetText(_data.Price);
        ToggleBuyContainer();
    }

    private void ToggleBuyContainer()
    {
        if (_data.IsPurchased)
        {
            _useText.gameObject.SetActive(true);
            _buyContainer.gameObject.SetActive(false);
        }
        else
        {
            _useText.gameObject.SetActive(false);
            _buyContainer.gameObject.SetActive(true);
        }
    }

    private void OnUsed()
    {
        if (_coinStorage.TrySpend(_data.Price))
        {
            _soundContainer.Play(SoundType.SpendCoin);
            _data.IsPurchased = true;
            ToggleBuyContainer();
        }

        Used?.Invoke(_index);
    }

    private void OnClicked() => Clicked?.Invoke(_data.FullDescription);
}