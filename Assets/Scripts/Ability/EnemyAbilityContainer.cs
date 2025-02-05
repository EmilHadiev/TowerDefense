using UnityEngine;

public class EnemyAbilityContainer : MonoBehaviour
{
    private void OnEnable()
    {
        if (Ability is IEnableAbility)
            Ability.Activate();
    }

    public IAbility Ability { get; private set; }

    private void Awake()
    {
        EnemyType type = GetComponent<Enemy>().Type;

        InitializeAbility(type);
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
