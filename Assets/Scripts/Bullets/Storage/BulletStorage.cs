using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletStorage : MonoBehaviour
{    
    [SerializeField] private PlayerCombatView _combatView;
    [SerializeField] private ShootPosition _shootPosition;
    [SerializeField] private int _poolSize = 30;
    [SerializeField] private int _additionalPoolSize = 5;

    private Bullet[] _bulletTemplates;

    private IAttackable _attacker;
    private List<IPool<Bullet>> _pool;
    private IInputSystem _input;
    private BulletEffectSetter _effectSetter;
    private IInstantiator _instantiator;

    private int _bulletIndex;

    private void OnValidate()
    {
        _combatView ??= GetComponent<PlayerCombatView>();
        _shootPosition ??= GetComponentInChildren<ShootPosition>();
    }

    private void OnEnable()
    {
        _attacker.Attacked += OnAttacked;
        _input.SwitchBulletButtonClicked += OnBulletClicked;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttacked;
        _input.SwitchBulletButtonClicked -= OnBulletClicked;
    }

    private void Start()
    {
        _pool = new List<IPool<Bullet>>(10);
        _effectSetter = new BulletEffectSetter();

        InitializeTemplatesAndPools();
        InitializeEffects();
    }

    private void InitializeEffects()
    {
        SetEffect(_bulletIndex);
        _input.SwitchTo(0);
    }

    private void InitializeTemplatesAndPools()
    {
        for (int i = 0; i < _bulletTemplates.Length; i++)
        {
            _pool.Add(new BulletPool());
            CreateBullets(_poolSize, i);
        }
    }

    [Inject]
    private void Constructor(IAttackable attacker, IInstantiator instantiator, IInputSystem inputSystem, Bullet[] bullets)
    {
        _attacker = attacker;
        _input = inputSystem;
        _bulletTemplates = bullets;
        _instantiator = instantiator;
    }

    private void SetParticleColor(Color color) => _combatView.SetParticleColor(color);

    private void CreateBullets(int poolSize, int index)
    {
        for (int i = 0; i < poolSize; i++)
            CreateTemplate(_bulletTemplates[index], _pool[index]);
    }

    private void CreateTemplate(Bullet template, IPool<Bullet> pool)
    {
        Bullet bullet = _instantiator.InstantiatePrefab(template).GetComponent<Bullet>();
        bullet.InitEffects(SetEffect);
        bullet.gameObject.SetActive(true);
        bullet.gameObject.SetActive(false);
        pool.Add(bullet);
        _effectSetter.AddBullet(bullet);
    }

    private void OnAttacked()
    {
        if (_pool[_bulletIndex].TryGet(out Bullet bullet))
        {
            bullet.transform.parent = null;
            bullet.transform.position = _shootPosition.Position;
            bullet.transform.rotation = _shootPosition.Rotation;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            CreateBullets(_additionalPoolSize, _bulletIndex);
        }
    }

    private void OnBulletClicked(int bulletIndex)
    {
        SetIAvailableIndex(bulletIndex);
        SetEffect(_bulletIndex);
    }

    private void SetIAvailableIndex(int bulletIndex)
    {
        if (bulletIndex < 0 || bulletIndex >= _bulletTemplates.Length)
            _bulletIndex = -1;
        else
            _bulletIndex = bulletIndex;

        if (IsPurchasedBullet(_bulletIndex) == false || _bulletIndex == -1)
            _bulletIndex = GetPurchasedIndex();
    }

    private bool IsPurchasedBullet(int bulletIndex) => _bulletTemplates[bulletIndex].BulletDescription.IsPurchased;

    private int GetPurchasedIndex()
    {
        for (int i = 0; i < _bulletTemplates.Length; i++)
            if (_bulletTemplates[i].BulletDescription.IsPurchased)
                return i;

        throw new ArgumentOutOfRangeException(nameof(_bulletTemplates));
    }

    private void SetEffect(int bulletIndex)
    {
        _effectSetter.SetBulletEffect(_bulletTemplates[bulletIndex].Type);
        SetParticleColor(_bulletTemplates[bulletIndex].Color);
    }
}