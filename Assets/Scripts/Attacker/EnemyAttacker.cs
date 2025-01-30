using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;

    private const float AdditionalY = 0.5f;

    private Collider[] _hits = new Collider[1];

    private void Hit()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _radius, _hits, _mask);

        if (hitCount == 0)
            return;

        for (int i = 0; i < hitCount; i++)
            if (_hits[i].TryGetComponent(out IHealth playerHealth))
                playerHealth.TakeDamage(_damage);

        PhysicsDebug.DrawDebug(GetStartPoint(), _radius);
    }

    private Vector3 GetStartPoint() => new Vector3(transform.position.x, transform.position.y + AdditionalY, transform.position.z) + transform.forward;

    //this event from animation
    private void AttackEnded() => Hit();
}