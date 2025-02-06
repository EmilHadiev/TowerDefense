using UnityEngine;

[RequireComponent(typeof(BulletStorage))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerViewStorage))]
public class Player : MonoBehaviour, IPlayer
{
    public Transform Transform => transform;
}