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
            UpdateData(_gunsData[i], _items[i]);
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

            _gunsData[i].BaseDamage = item.BaseDamage;
            _gunsData[i].AttackSpeedPercent =(int)item.AttackSpeedPercent;
            _gunsData[i].DamageLevel = item.DamageLevel;
            _gunsData[i].AttackSpeedLevel = item.AttackSpeedLevel;
            _gunsData[i].IsDropped = item.IsDropped;
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
                IsDropped = _gunsData[i].IsDropped,
                AttackSpeedPercent = _gunsData[i].AttackSpeedPercent,
                BaseDamage = _gunsData[i].BaseDamage,
                DamageLevel = _gunsData[i].DamageLevel,
                AttackSpeedLevel = _gunsData[i].AttackSpeedLevel
            };

            _items.Add(item);
        }
    }

    private void UpdateData(GunData gun, GunItem gunItem)
    {
        gunItem.IsDropped = gun.IsDropped;
        gunItem.BaseDamage = gun.BaseDamage;
        gunItem.AttackSpeedPercent = gun.AttackSpeedPercent;
        gunItem.DamageLevel = gun.DamageLevel;
        gunItem.AttackSpeedLevel = gun.AttackSpeedLevel;
    }
}
