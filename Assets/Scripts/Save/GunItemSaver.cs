using System.Collections.Generic;
using System.Linq;

public class GunItemSaver
{
    private readonly GunData[] _gunsData;
    private readonly List<GunItem> _items;

    public GunItemSaver(GunData[] guns, List<GunItem> gunsItems)
    {
        _gunsData = guns;
        _items = gunsItems;
        Init();
    }

    public void Save()
    {
        for (int i = 0; i < _items.Count; i++)
            UpdateAvailable(_gunsData[i], _items[i]);
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
        UnityEngine.Debug.Log("Default init for guns!");
        var itemsDict = _items.ToDictionary(item => item.Id, item => item);

        for (int i = 0; i < _gunsData.Length; i++)
        {
            if (itemsDict.TryGetValue(_gunsData[i].ID, out GunItem item) == false)
            {
                throw new System.NullReferenceException($"GunItem with ID {_gunsData[i].ID} not found in _items");
            }

            _gunsData[i].IsPurchased = item.IsPurchased;
        }
    }

    private void FirstInit()
    {
        UnityEngine.Debug.Log("first init for guns!");
        for (int i = 0; i < _gunsData.Length; i++)
        {
            var item = new GunItem
            {
                Id = _gunsData[i].ID,
                IsPurchased = _gunsData[i].IsPurchased
            };

            _items.Add(item);
        }
    }

    private void UpdateAvailable(IPurchasable gun, GunItem gunItem)
    {
        gunItem.IsPurchased = gun.IsPurchased;
    }
}
