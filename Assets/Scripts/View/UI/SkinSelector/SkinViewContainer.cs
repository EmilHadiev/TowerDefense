using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinViewContainer : MonoBehaviour
{
    [SerializeField] private SkinView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _selectButton;

    private PlayerData[] _playerData;
    private readonly List<SkinView> _views = new List<SkinView>(9);
    private PlayerData _currentData;
    private ISceneLoader _sceneLoader;
    private EnvironmentData _envData;

    [Inject]
    private void Constructor(PlayerData[] playerData, ISceneLoader sceneLoader, EnvironmentData environmentData)
    {
        _playerData = playerData;
        _sceneLoader = sceneLoader;
        _envData = environmentData;
    }

    private void Awake()
    {
        CreatePrefabs();
    }

    private void OnEnable()
    {
        foreach (var view in _views)
            view.Selected += OnSkinSelected;

        _selectButton.onClick.AddListener(SelectSkin);
    }

    private void OnDisable()
    {
        foreach (var view in _views)
            view.Selected += OnSkinSelected;

        _selectButton.onClick.AddListener(SelectSkin);
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
    }

    private void SelectSkin()
    {
        _envData.PlayerData = _currentData;
        _sceneLoader.SwitchTo(2);
    }
}
