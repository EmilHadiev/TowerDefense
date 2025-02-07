using UnityEngine;
using Zenject;

public class EnemyAbilityAdder : MonoBehaviour
{
    private IInstantiator _instantiator;

    private void Awake()
    {
        EnemyType type = GetComponent<Enemy>().Type;

        InitializeAbility(type);
    }

    [Inject]
    private void Constructor(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void InitializeAbility(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.ArmorKnight:
                _instantiator.InstantiateComponent<ArmorKnightAbility>(gameObject);
                break;
            case EnemyType.Turtle:
                _instantiator.InstantiateComponent<TurtleAbility>(gameObject);
                break;
            case EnemyType.Mage:
                _instantiator?.InstantiateComponent<MageAbility>(gameObject);
                break;
            default:                
                break;
        }
    }
}