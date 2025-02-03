using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    private const float AdditionalY = 0.5f;

    private Collider[] _hits = new Collider[1];

    private EnemyStat _stat;

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;
    }

    private void Hit()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _stat.AttackRadius, _hits, _mask);

        if (hitCount == 0)
            return;

        for (int i = 0; i < hitCount; i++)
            if (_hits[i].TryGetComponent(out IHealth playerHealth))
                playerHealth.TakeDamage(0);

        PhysicsDebug.DrawDebug(GetStartPoint(), _stat.AttackRadius);
    }

    private Vector3 GetStartPoint() => new Vector3(transform.position.x, transform.position.y + AdditionalY, transform.position.z) + transform.forward;

    //this event from animation
    private void AttackEnded() => Hit();
}