using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AwardGiver : MonoBehaviour
{
    private const int SkipBullet = 1;
    private const int SkipGun = 2;

    private const int AwardStep = 6;

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
    private void Constructor(Bullet[] bullets, GunData[] gunData)
    {
        _bulletData = bullets.Skip(SkipBullet).ToArray();
        _gunData = gunData.Skip(SkipGun).ToArray();
    }

    public void GiveAward()
    {
        
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