using UnityEngine;

[RequireComponent(typeof(ObstacleHealth))]
public class Flamethrower : InteractiveObstacle
{
    [SerializeField] private ObstacleHealth _health;
    [SerializeField] private FlamethrowerAttacker _attacker;

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
        _attacker ??= GetComponentInChildren<FlamethrowerAttacker>();
    }

    protected override void Activate(Collider collider)
    {
        _attacker.Activate();
    }
}