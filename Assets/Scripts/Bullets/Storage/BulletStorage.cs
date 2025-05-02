using UnityEngine;
using Zenject;

public class BulletStorage : MonoBehaviour
{    
    [SerializeField] private PlayerCombatView _combatView;
    [SerializeField] private ShootPosition _shootPosition;
    [SerializeField] private int _poolSize = 30;
    [SerializeField] private int _additionalPoolSize = 5;

    private IAttackable _attacker;
    private BulletEffectSetter _effectSetter;
    private BulletCreator _bulletsCreator;
    private IBulletsSelector _indexator;

    private int SelectIndex => _indexator.SelectBulletIndex;

    private void OnValidate()
    {
        _combatView ??= GetComponent<PlayerCombatView>();
        _shootPosition ??= GetComponentInChildren<ShootPosition>();
    }

    private void OnEnable()
    {
        _attacker.Attacked += OnAttacked;
        _indexator.BulletSwitched += OnBulletSwitched;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttacked;

        _indexator.BulletSwitched -= OnBulletSwitched;
        _indexator.Dispose();
    }

    private void Start()
    {
        _bulletsCreator.FirstInit(_poolSize);
        InitializeEffects();
    }

    [Inject]
    private void Constructor(IAttackable attacker, IInstantiator instantiator, IInputHandler inputSystem, Bullet[] bullets)
    {
        _attacker = attacker;
        _effectSetter = new BulletEffectSetter();
        BulletPoolContainer poolContainer = new BulletPoolContainer();
        _bulletsCreator = new BulletCreator(instantiator, bullets, poolContainer, _effectSetter, SetEffect);
        _indexator = new BulletsSelector(inputSystem, bullets);
    }

    private void InitializeEffects()
    {
        SetEffect(SelectIndex);
    }

    private void SetEffect(int bulletIndex)
    {
        _effectSetter.SetBulletEffect(GetBulletByIndex(bulletIndex).Type);
        _combatView.SetParticleColor(GetBulletByIndex(bulletIndex).Color);
    }

    private Bullet GetBulletByIndex(int index) => _bulletsCreator[index];

    private void OnAttacked()
    {
        if (_bulletsCreator.TryGetSelectBullet(SelectIndex, out Bullet bullet))
        {            
            bullet.transform
                .SetPositionAndRotation(_shootPosition.Position, _shootPosition.Rotation);
            bullet.gameObject.SetActive(true);
        }
        else
        {
            _bulletsCreator.CreateBullets(_additionalPoolSize, SelectIndex);
        }
    }

    private void OnBulletSwitched(int bulletIndex) => SetEffect(SelectIndex);
}