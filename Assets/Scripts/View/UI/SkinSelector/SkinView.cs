using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _container;
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _backgroundImage;

    public event Action<PlayerData> Selected;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
        CreatePrefab();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void Select()
    {
        Selected?.Invoke(_playerData);
        BackgroundImageToggle(true);
    }

    private void CreatePrefab()
    {
        var prefab = Instantiate(_playerData.StickmanView, _container);
    }

    public void BackgroundImageToggle(bool isOn)
    {
        _borderImage.enabled = isOn;
    }

    public void TryChangeBackground(PlayerData playerData)
    {
        if (playerData == _playerData)
            _backgroundImage.color = new Color(1, 0.8431373f, 0, 1);
        else
            _backgroundImage.color = new Color(0.254902f, 0.2352941f, 0.3529412f, 1);           
    }
}