using UnityEngine;

public class SphereColliderConfigurator : IComponentConfigurator
{
    private const float YPosition = 0.5f;
    private const int DefaultRadius = 1;

    private readonly SphereCollider _collider;

    public SphereColliderConfigurator(SphereCollider collider)
    {
        _collider = collider;
    }

    public void Configurate()
    {
        _collider.center = new Vector3(0, YPosition, 0);
        _collider.radius = DefaultRadius;
    }
}