using UnityEngine;
using Zenject;

public class EnemyAbilityVisitor : IEnemyVisitor
{
    private readonly IInstantiator _instantiator;
    private readonly GameObject _gameObject;

    public EnemyAbilityVisitor(IInstantiator instantiator, GameObject gameObject)
    {
        _instantiator = instantiator;
        _gameObject = gameObject;
    }
    
    public void Visit(Mage enemy)
    {
        Debug.Log("Это маг!");
        _instantiator?.InstantiateComponent<MageAbility>(_gameObject);
    }

    public void Visit(ArmorKnight armorKnight)
    {
        Debug.Log("Это рыцарь");
        _instantiator.InstantiateComponent<ArmorKnightAbility>(_gameObject);
    }

    public void Visit(Turtle turtle)
    {
        Debug.Log("Это черепаха");
        _instantiator.InstantiateComponent<TurtleAbility>(_gameObject);
    }

    public void Visit(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}