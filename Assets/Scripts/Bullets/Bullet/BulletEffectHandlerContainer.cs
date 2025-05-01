using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletEffectHandlerContainer : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private PlayerStat _playerStat;
    private ICoinStorage _coinStorage;
    private IHealth _playerHealth;
    private Action<int> _setEffect;

    private Dictionary<Type, IBulletEffectHandler> _effects;

    public IReadOnlyDictionary<Type, IBulletEffectHandler> Effects => _effects;

    private void OnValidate()
    {
        _bullet ??= GetComponent<Bullet>();
    }

    public void SetEffects(Action<int> setEffect)
    {
        _setEffect = setEffect;
        InitEffects();
    }

    [Inject]
    private void Constructor(IPlayer player, ICoinStorage coinStorage, PlayerStat playerStat)
    {
        _playerHealth = player.Health;
        _coinStorage = coinStorage;
        _playerStat = playerStat;
    }

    private void InitEffects()
    {
        _effects = new Dictionary<Type, IBulletEffectHandler>(10)
        {
            [typeof(EmptyBulletEffect)] = new EmptyBulletEffect(),
            [typeof(SlowdownBulletEffect)] = new SlowdownBulletEffect(),
            [typeof(ExtraDamageBulletEffect)] = new ExtraDamageBulletEffect(_bullet.Data, _playerStat),
            [typeof(SplashBulletEffect)] = new SplashBulletEffect(transform, _bullet.Data, _playerStat),
            [typeof(PushingBulletEffect)] = new PushingBulletEffect(),
            [typeof(DeadlyBulletEffect)] = new DeadlyBulletEffect(),
            [typeof(PoisonBulletEffect)] = new PoisonBulletEffect(),
            [typeof(VampirismEffect)] = new VampirismEffect(_playerHealth, _playerStat),
            [typeof(GoldenBulletEffect)] = new GoldenBulletEffect(_coinStorage),
            [typeof(RandomBulletEffect)] = new RandomBulletEffect(_setEffect)
        };
    }
}
