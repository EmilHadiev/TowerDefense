using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataSaver
{
    private readonly List<PlayerDataItem> _items;
    private readonly PlayerData[] _playerData;

    public PlayerDataSaver(PlayerData[] playerData, List<PlayerDataItem> items)
    {
        _items = items;
        _playerData = playerData;
        Init();
    }

    public void Save()
    {
        for (int i = 0; i < _items.Count; i++)
            UpdateAvailable( _playerData[i], _items[i]);
    }

    private void Init()
    {
        if (_items.Count == 0)
            FirstInit();
        else
            DefaultInit();
    }

    private void DefaultInit()
    {
        Debug.Log("Default init for player data");
        var itemsDict = _items.ToDictionary(item => item.Id, item => item);

        for (int i = 0; i < _playerData.Length; i++)
        {
            if (itemsDict.TryGetValue(_playerData[i].Id, out PlayerDataItem item) == false)
            {
                throw new System.NullReferenceException($"GunItem with ID {_playerData[i].Id} not found in _items");
            }

            Debug.Log($"{item.Id} ~ {item.IsPurchased}");

            _playerData[i].IsPurchased = item.IsPurchased;
        }
    }

    private void FirstInit()
    {
        UnityEngine.Debug.Log("first init for guns!");
        for (int i = 0; i < _playerData.Length; i++)
        {
            var item = new PlayerDataItem
            {
                Id = _playerData[i].Id,
                IsPurchased = _playerData[i].IsPurchased
            };

            _items.Add(item);
        }
    }

    private void UpdateAvailable(IPurchasable playerData, PlayerDataItem playerDataItem)
    {
        playerDataItem.IsPurchased = playerData.IsPurchased;
    }
}