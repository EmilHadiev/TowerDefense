using UnityEngine;

public class RigidBodyConfigurator : IComponentConfigurator
{
    private readonly Rigidbody _rigidBody;

    public RigidBodyConfigurator(Rigidbody rigidbody)
    {
        _rigidBody = rigidbody;
    }

    public void Configurate()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.constraints = GetConstraints();
    }

    private RigidbodyConstraints GetConstraints() =>
        RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
}
