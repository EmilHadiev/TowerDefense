using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using static UnityEditor.Progress;

public class YandexSaver : ISavable, IDisposable
{
    private readonly ICoinStorage _coinStorage;
    private readonly IEnumerable<UpgradeData> _data;

    private int Coins
    {
        get => YG2.saves.coins;

        set => YG2.saves.coins = value;
    }

    private List<SavesYG.UpgradeItem> _items = new List<SavesYG.UpgradeItem>(3);

    public YandexSaver(ICoinStorage coinStorage, IEnumerable<UpgradeData> data)
    {
        _coinStorage = coinStorage;
        _data = data;
    }

    public void Dispose()
    {
        SaveProgress();
        ResetAllSavesAndProgress();
    }

    public void InitProgress()
    {
        Debug.Log("Первый запуск!");
    }

    public void LoadProgress()
    {
        LoadCoins();
        LoadUpgraders();
    }

    public void ResetAllSavesAndProgress() => YG2.SetDefaultSaves();

    public void SaveProgress()
    {
        SaveData();
        YG2.SaveProgress();
    }

    private void SaveData()
    {
        SaveCoins();
        SaveUpgraders();
    }

    #region Coins
    private void LoadCoins()
    {
        if (Coins != _coinStorage.Coins)
            _coinStorage.Add(Coins);
    }
    private void SaveCoins() => Coins = _coinStorage.Coins;
    #endregion

    #region Upgraders
    private void LoadUpgraders()
    {
        InitItems();
        SetCosts();
    }

    private void InitItems()
    {
        _items = YG2.saves.UpgradeItems;

        if (_items.Count == 0 || _items == null)
            FirstItemsInit();

        if (_items.Count != _data.Count())
            UpdateSourceItems(ref _items, _data);
    }

    private void FirstItemsInit()
    {
        foreach (var data in _data)
        {
            var item = new SavesYG.UpgradeItem() 
            { 
                Cost = data.Cost, 
                Type = data.UpgradeType
            };

            _items.Add(item);
        }
    }

    private void UpdateSourceItems(ref List<SavesYG.UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        AddMissingItem(items, data);
        RemoveUnnecessaryItems(items, data);
    }

    private void RemoveUnnecessaryItems(List<SavesYG.UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        foreach (var upgrade in data)
        {
            if (items.Any(item => item.Type == upgrade.UpgradeType) == false)
            {
                var upgradeItem = new SavesYG.UpgradeItem()
                {
                    Cost = upgrade.Cost,
                    Type = upgrade.UpgradeType
                };

                items.Add(upgradeItem);
            }
        }
    }

    private void AddMissingItem(List<SavesYG.UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        items.RemoveAll(item => data.Any(upgradeData => upgradeData.UpgradeType == item.Type) == false);
    }

    private void SetCosts()
    {
        foreach (var data in _data)
            SetCost(data);
    }

    private void SetCost(UpgradeData data)
    {
        foreach (var item in _items)
        {
            if (data.UpgradeType == item.Type)
            {
                data.Cost = item.Cost;
                return;
            }
        }
    }

    private void SaveUpgraders()
    {
        for (int i = 0; i < _items.Count; i++)
            UpdateCost(i);
    }

    private void UpdateCost(int index)
    {
        foreach (var data in _data)
            if (data.UpgradeType == _items[index].Type)
                _items[index].Cost = data.Cost;
    }

    #endregion
}