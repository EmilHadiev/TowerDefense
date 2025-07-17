using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using Zenject;

public class AwardGiver : MonoBehaviour
{
    private const int SkipBullet = 1;
    private const int SkipGun = 2;

    private const int AwardStep = 6;

    private LevelTracker _levelTracker;

    private IBulletDefinition[] _bulletData;
    private GunData[] _gunData;

    private readonly Dictionary<int, IBulletDefinition> _bullets = new Dictionary<int, IBulletDefinition>(10);
    private readonly Dictionary<int, GunData> _guns = new Dictionary<int, GunData>(10);

    public IReadOnlyDictionary<int, IBulletDefinition> Bullets => _bullets;
    public IReadOnlyDictionary<int, GunData> Guns => _guns;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        PrepareBulletData();
        PrepareGunData();
    }

    [Inject]
    private void Constructor(Bullet[] bullets, GunData[] gunData, LevelTracker levelTracker)
    {
        _levelTracker = levelTracker;
        _bulletData = bullets.Skip(SkipBullet).ToArray();
        _gunData = gunData.Skip(SkipGun).ToArray();
    }

    public void GiveReward()
    {
        ILootable lootable = GetCurrentAward();
        lootable.IsDropped = true;
    }

    public bool IsRewardLevel()
    {
        ILootable lootable = GetCurrentAward();

        if (lootable == null || _levelTracker.IsNotCompletedLevel == false)
            return false;

        return true;
    }

    public string GetRewardDescription()
    {
        int completedLevel = _levelTracker.NumberLevelsCompleted;

        if (Guns.TryGetValue(completedLevel, out GunData gunData))
            return gunData.GetLocalizedText(YG2.lang).Name;

        if (Bullets.TryGetValue(completedLevel, out IBulletDefinition bulletData))
            return bulletData.BulletDescription.GetLocalizedText(language: YG2.lang).Name;

        Debug.LogError($"{completedLevel} does not contain a reward");
        return "";
    }

    public Sprite GetRewardSprite()
    {
        int completedLevel = _levelTracker.NumberLevelsCompleted;

        if (Guns.TryGetValue(completedLevel, out GunData gunData))
            return gunData.Sprite;

        if (Bullets.TryGetValue(completedLevel, out IBulletDefinition bulletData))
            return bulletData.BulletDescription.Sprite;

        Debug.LogError($"{completedLevel} does not contain a reward");
        return null;
    }

    private ILootable GetCurrentAward()
    {
        int completedLevel = _levelTracker.NumberLevelsCompleted;

        if (Guns.TryGetValue(completedLevel, out GunData gunData))
            return gunData;

        if (Bullets.TryGetValue(completedLevel, out IBulletDefinition bulletData))
            return bulletData.BulletDescription;

        return null;
    }

    private void PrepareGunData()
    {
        int indexator = Constants.GiveFirstGunAwardLevel;

        for (int i = 0; i < _gunData.Length; i++)
        {
            _guns.Add(indexator, _gunData[i]);
            indexator += AwardStep;
        }
    }

    private void PrepareBulletData()
    {
        int indexator = Constants.GiveFirstBulletAwardLevel;

        for (int i = 0; i < _bulletData.Length; i++)
        {
            _bullets.Add(indexator, _bulletData[i]);
            indexator += AwardStep;
        }
    }
}