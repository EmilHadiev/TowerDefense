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
                
                break;
            case EnemyType.Turtle:
                _instantiator.InstantiateComponent<TurtleAbility>(gameObject);
                break;
            default:
                
                break;
        }
    }
}
