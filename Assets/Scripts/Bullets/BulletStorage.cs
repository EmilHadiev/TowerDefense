using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

public class BulletStorage : MonoBehaviour
{
    [SerializeField] private Bullet[] _bulletTemplates;
    [SerializeField] private PlayerViewStorage _playerViewStorage;
    [SerializeField] private int _poolSize;
    [SerializeField] private int _additionalPoolSize;

    private IAttackable _attacker;
    private List<IPool<Bullet>> _pool;
    private ISoundContainer _soundContainer;
    private IDesktopInput _desktopInput;
    private BulletEffectSetter _effectSetter;
    private PlayerStat _playerStat;

    private int _bulletIndex;

    private void OnEnable()
    {
        _attacker.Attacked += OnAttacked;
        _desktopInput.SwitchBulletButtonClicked += OnBulletClicked;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttacked;
        _desktopInput.SwitchBulletButtonClicked -= OnBulletClicked;
    }

    private void Start()
    {
        _pool = new List<IPool<Bullet>>(10);
        _effectSetter = new BulletEffectSetter();

        InitializeTemplatesAndPools();

        SetEffect(_bulletIndex);
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
    private void Constructor(IAttackable attacker, ISoundContainer soundContainer, IDesktopInput desktopInput, PlayerStat playerStat)
    {
        _attacker = attacker;
        _soundContainer = soundContainer;
        _desktopInput = desktopInput;
        _playerStat = playerStat;
    }

    private void SetParticleColor(Color color) => _playerViewStorage.SetParticleViewColor(color);

    private void CreateBullets(int poolSize, int index)
    {
        for (int i = 0; i < poolSize; i++)
            CreateTemplate(_bulletTemplates[index], _pool[index]);
    }

    private void CreateTemplate(Bullet template, IPool<Bullet> pool)
    {
        Bullet bullet = Instantiate(template);
        bullet.InitPlayerDamage(_playerStat);
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
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
            _soundContainer.Play(bullet.Type);
        }
        else
        {
            CreateBullets(_additionalPoolSize, _bulletIndex);
        }
    }

    private void OnBulletClicked(int bulletIndex)
    {
        if (bulletIndex < 0 || bulletIndex > _bulletTemplates.Length)
            throw new ArgumentOutOfRangeException(nameof(bulletIndex));

        _bulletIndex = bulletIndex;
        SetEffect(_bulletIndex);

        _soundContainer.Play(SoundType.SwitchBullet);
    }

    private void SetEffect(int bulletIndex)
    {
        _effectSetter.SetBulletEffect(_bulletTemplates[bulletIndex].Type); 
        SetParticleColor(_bulletTemplates[bulletIndex].Color);
    }
}