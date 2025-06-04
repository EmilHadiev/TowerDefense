using UnityEngine;

[RequireComponent(typeof(BulletStorage))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerCombatView))]
[RequireComponent(typeof(Resurrector))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private Resurrector _resurrector;
    [SerializeField] private GunPlace _gunPlace;

    [field: SerializeField] public PlayerType Type { get; private set; }

    protected virtual void OnValidate()
    {
        _health ??= GetComponent<PlayerHealth>();
        _resurrector ??= GetComponent<Resurrector>();
        _gunPlace ??= GetComponentInChildren<GunPlace>();
    }

    public Transform Transform => transform;

    public IHealth Health => _health;

    public IResurrectable Resurrectable => _resurrector;

    public IGunPlace GunPlace => _gunPlace;
}