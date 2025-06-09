using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _container;
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
        Selected?.Invoke(_playerData);
        BackgroundImageToggle(true);
    }

    private void CreatePrefab()
    {
        var prefab = Instantiate(_playerData.StickmanView, _container);
    }

    public void BackgroundImageToggle(bool isOn)
    {
        _backgroundImage.enabled = isOn;
    }
}