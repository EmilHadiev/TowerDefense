using UnityEngine;

public interface IBulletPolicy
{
    void Accept(Collider enemy);
}