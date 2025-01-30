using UnityEngine;

[RequireComponent(typeof(AmmunitionStorage))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, IPlayer
{
    public Transform Transform => transform;
}
