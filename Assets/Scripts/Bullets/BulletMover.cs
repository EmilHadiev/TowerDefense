using System;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private BulletData _data;

    private IBulletMover _mover;

    public event Action<Vector3, ReflectiveObstacle> Collided;

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
            Vector3 normal = collision.contacts[0].normal;
            var direction = Vector3.Reflect(_mover.Direction, normal).normalized;

            Collided?.Invoke(direction, wall);
            Debug.Log("Проверка");
        }
    }
}