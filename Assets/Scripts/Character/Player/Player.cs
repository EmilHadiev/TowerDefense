using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AmmunitionStorage))]
public class Player : MonoBehaviour, IPlayer
{
    public Transform Transform => transform;
}
