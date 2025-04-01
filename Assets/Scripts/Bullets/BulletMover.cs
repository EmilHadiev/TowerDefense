using System;
using UnityEngine;

public class BulletMover : MonoBehaviour, IBulletMovable
{
    [SerializeField] private BulletData _data;

    private IBulletMover _mover;

    public event Action<Vector3, ReflectiveObstacle> Collided;
    public event Action Reflected;

    private void Awake() => _mover = new BulletReflectMovePattern(_data, transform, this);

    private void OnEnable()
    {
        _mover.StartMove();
        Collided?.Invoke(transform.forward, null);
    }

    private void OnDisable() => _mover.StopMove();

    private void Update() => _mover.Update();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out ReflectiveObstacle wall))
        {
            Vector3 direction = GetReflectedDirection(collision);
            Collided?.Invoke(direction, wall);
            Reflected?.Invoke();
        }
    }

    private Vector3 GetReflectedDirection(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        var direction = Vector3.Reflect(_mover.Direction, normal).normalized;
        direction.y = 0;

        return direction;
    }
}