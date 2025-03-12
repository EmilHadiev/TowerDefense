using System;
using YG;
using Zenject;

public class YandexSaver : ISavable, IDisposable
{
    private readonly ICoinStorage _coinStorage;

    public int Coins
    {
        get => YG2.saves.coins;

        set => YG2.saves.coins = value;
    }

    public YandexSaver(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    public void Dispose()
    {
        SaveData();
        SaveProgress();
    }
    
    public void LoadProgress()
    {
        _coinStorage.Add(Coins);
    }

    public void ResetAllSavesAndProgress() => YG2.SetDefaultSaves();

    public void SaveProgress() => YG2.SaveProgress();

    private void SaveData()
    {
        Coins = _coinStorage.Coins;
    }
}