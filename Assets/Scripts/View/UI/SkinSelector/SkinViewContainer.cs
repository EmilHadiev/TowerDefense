using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinViewContainer : MonoBehaviour
{
    [SerializeField] private SkinView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _selectText;
    [SerializeField] private TMP_Text _priceText;

    private PlayerData[] _playerData;
    private readonly List<SkinView> _views = new List<SkinView>(9);
    private PlayerData _currentData;
    private ISceneLoader _sceneLoader;
    private IPurchaser _purchaser;
    private EnvironmentData _envData;
    private ICoinStorage _coinStorage;
    private ISavable _savable;

    [Inject]
    private void Constructor(PlayerData[] playerData, ISceneLoader sceneLoader, EnvironmentData environmentData, IPurchaser purchaser, ISavable savable)
    {
        _playerData = playerData;
        _sceneLoader = sceneLoader;
        _envData = environmentData;
        _purchaser = purchaser;
        _savable = savable;
    }

    private void Awake() => CreatePrefabs();

    private void OnEnable()
    {
        foreach (var view in _views)
            view.Selected += OnSkinSelected;

        _selectButton.onClick.AddListener(SelectSkin);
    }

    private void OnDisable()
    {
        foreach (var view in _views)
            view.Selected -= OnSkinSelected;

        _selectButton.onClick.RemoveListener(SelectSkin);
    }

    private void CreatePrefabs()
    {
        for (int i = 0; i < _playerData.Length; i++)
        {
            var prefab = Instantiate(_template, _container);
            prefab.Init(_playerData[i]);
            _views.Add(prefab);
        }
    }

    private void OnSkinSelected(PlayerData data)
    {
        foreach (var view in _views)
            view.BackgroundImageToggle(false);

        _currentData = data;
        TextToggle();
    }

    private void TextToggle()
    {
        if (_currentData.IsPurchased)
        {
            _priceText.gameObject.SetActive(false);
            _selectText.gameObject.SetActive(true);
        }
        else
        {
            _priceText.gameObject.SetActive(true);
            _priceText.text = _currentData.Price.ToString();

            _selectText.gameObject.SetActive(false);
        }
    }

    private void SelectSkin()
    {
        if (_currentData.IsPurchased == false)
        {
            if (_purchaser.TryPurchase(_currentData))
            {
                _currentData.IsPurchased = true;
                TextToggle();
                _savable.SaveProgress();
            }
            return;
        }

        _envData.PlayerData = _currentData;
        _sceneLoader.SwitchTo(AssetProvider.SceneDefaultArena);
    }
}
