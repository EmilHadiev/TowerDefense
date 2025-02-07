using System;
using UnityEngine;

public class EnemyAbilityContainer : MonoBehaviour
{
    [SerializeField] private EnemyDieChecker _dieChecker;
    [SerializeField] private EnemyHealth _health;

    private bool _isInit = false;

    public IAbility Ability { get; private set; }

    private void OnValidate()
    {
        _dieChecker ??= GetComponent<EnemyDieChecker>();
        _health ??= GetComponent<EnemyHealth>();
    }

    private void Awake()
    {
        EnemyType type = GetComponent<Enemy>().Type;

        InitializeAbility(type);
    }

    private void OnEnable()
    {
        _health.Died += OnDied;

        if (Ability is IEnableAbility)
            Ability.Activate();
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        if (_isInit == false)
            return;

        if (Ability is IDisableAbility)
            Ability.Activate();
    }

    private void InitializeAbility(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.ArmorKnight:
                Ability = new EnemyMirror(transform);
                break;
            case EnemyType.Turtle:
                Ability = new EnemyExplosion(transform);
                _isInit = true;
                break;
            default:
                Ability = new EmptyAbility();
                break;
        }
    }
}
