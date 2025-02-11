using UnityEngine;

[RequireComponent(typeof(BulletStorage))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerViewStorage))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerHealth _health;

    private void OnValidate() => _health ??= GetComponent<PlayerHealth>();

    public Transform Transform => transform;

    public IHealth Health => _health;
}