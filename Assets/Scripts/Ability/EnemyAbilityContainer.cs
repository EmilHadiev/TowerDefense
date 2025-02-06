using UnityEngine;

public class EnemyAbilityContainer : MonoBehaviour
{
    [SerializeField] private EnemyDieChecker _dieChecker;

    public IAbility Ability { get; private set; }

    private void OnValidate()
    {
        _dieChecker ??= GetComponent<EnemyDieChecker>();
    }

    private void Awake()
    {
        EnemyType type = GetComponent<Enemy>().Type;

        InitializeAbility(type);
    }

    private void OnEnable()
    {
        if (Ability is IEnableAbility)
            Ability.Activate();
    }

    private void OnDisable()
    {

    }

    private void InitializeAbility(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.ArmorKnight:
                Ability = new EnemyMirror(transform);
                break;
            default:
                Ability = new EmptyAbility();
                break;
        }
    }
}
