using System.Collections.Generic;
using System.Linq;

public class UpgradeItemsSaver
{
    private readonly IEnumerable<UpgradeData> _data;
    private List<UpgradeItem> _items;

    public UpgradeItemsSaver(List<UpgradeItem> upgradeItems, IEnumerable<UpgradeData> data)
    {
        _items = upgradeItems;
        _data = data;

        UpdateItems();
        SetCosts();
    }

    public void Save()
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

    private void UpdateItems()
    {
        if (_items.Count == 0 || _items == null)
            FirstItemsInit();

        if (_items.Count != _data.Count())
            UpdateSourceItems(ref _items, _data);
    }

    private void FirstItemsInit()
    {
        foreach (var data in _data)
        {
            var item = new UpgradeItem()
            {
                Cost = data.Cost,
                Type = data.UpgradeType
            };

            _items.Add(item);
        }
    }

    private void UpdateSourceItems(ref List<UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        AddMissingItems(items, data);
        RemoveUnnecessaryItems(items, data);
    }

    private void AddMissingItems(List<UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        items.RemoveAll(item => data.Any(upgradeData => upgradeData.UpgradeType == item.Type) == false);
    }

    private void RemoveUnnecessaryItems(List<UpgradeItem> items, IEnumerable<UpgradeData> data)
    {
        foreach (var upgrade in data)
        {
            if (items.Any(item => item.Type == upgrade.UpgradeType) == false)
            {
                var upgradeItem = new UpgradeItem()
                {
                    Cost = upgrade.Cost,
                    Type = upgrade.UpgradeType
                };

                items.Add(upgradeItem);
            }
        }
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
}