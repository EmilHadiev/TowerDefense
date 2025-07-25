using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class TeleportTrigger : MonoBehaviour
{
    public event Action<Enemy> EnemyEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyEntered?.Invoke(enemy);
    }
}