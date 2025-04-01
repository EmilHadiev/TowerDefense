using System;
using UnityEngine;

public interface IBulletMovable
{
    event Action<Vector3, ReflectiveObstacle> Collided;
    event Action Reflected;
}