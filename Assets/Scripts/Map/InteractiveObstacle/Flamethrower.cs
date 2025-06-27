using UnityEngine;

[RequireComponent(typeof(ObstacleHealth))]
public class Flamethrower : InteractiveObstacle
{
    [SerializeField] private ObstacleHealth _health;

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    protected override void Activate(Collider collider)
    {

    }
}