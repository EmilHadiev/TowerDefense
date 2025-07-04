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
    private IGunPlace _gunPlace;
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
        _gunPlace = player.GunPlace;
    }

    private void InitEffects()
    {
        _effects = new Dictionary<Type, IBulletEffectHandler>(10)
        {
            [typeof(EmptyBulletEffect)] = new EmptyBulletEffect(),
            [typeof(SlowdownBulletEffect)] = new SlowdownBulletEffect(),
            [typeof(ExtraDamageBulletEffect)] = new ExtraDamageBulletEffect(_gunPlace),
            [typeof(SplashBulletEffect)] = new SplashBulletEffect(_gunPlace, transform),
            [typeof(PushingBulletEffect)] = new PushingBulletEffect(transform),
            [typeof(DeadlyBulletEffect)] = new DeadlyBulletEffect(),
            [typeof(PoisonBulletEffect)] = new PoisonBulletEffect(),
            [typeof(VampirismEffect)] = new VampirismEffect(_playerHealth, _gunPlace),
            [typeof(GoldenBulletEffect)] = new GoldenBulletEffect(_coinStorage),
            [typeof(RandomBulletEffect)] = new RandomBulletEffect(_setEffect)
        };
    }
}
