using UnityEngine;

[RequireComponent(typeof(BulletStorage))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerCombatView))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerHealth _health;

    [field: SerializeField] public PlayerType Type { get; private set; }

    protected virtual void OnValidate() => _health ??= GetComponent<PlayerHealth>();

    public Transform Transform => transform;

    public IHealth Health => _health;
}