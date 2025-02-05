using UnityEngine;

[RequireComponent(typeof(BulletStorage))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, IPlayer
{
    public Transform Transform => transform;
}
