using UnityEngine;

public interface IBulletMover : IMover
{
    Vector3 Direction { get; set; }
}