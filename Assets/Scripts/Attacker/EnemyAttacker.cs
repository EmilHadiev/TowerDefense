using UnityEngine;
using Zenject;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private EnemyAttackZone _attackZone;

    private const float AdditionalY = 0.5f;
    private const int MaxTargets = 2;

    private LayerMask _layers;
    private IPlayer _player;
    private IEnemySoundContainer _soundContainer;

    private readonly Collider[] _hits = new Collider[MaxTargets];

    private EnemyStat _stat;

    private void OnValidate()
    {
        _attackZone ??= GetComponentInChildren<EnemyAttackZone>();
    }

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;

        _layers = LayerMask.GetMask(Constants.PlayerMask, Constants.ObstacleMask);
    }

    [Inject]
    private void Constructor(IEnemySoundContainer enemySoundContainer, IPlayer player)
    {
        _soundContainer = enemySoundContainer;
        _player = player;
    }

    private void Hit()
    {
        int hitCount = DetectTargets();

        if (hitCount == 0)
            return;

        AttackTargets(hitCount);

        ResetTarget();
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0)
            LookToTarget(); ;
    }

    private void AttackTargets(int hitCount)
    {
        for (int i = 0; i < hitCount; i++)
        {
            if (_hits[i].TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_stat.Damage);
                PlaySound();
            }
        }
    }

    private void LookToTarget()
    {
        transform.LookAt(_player.Transform.position);
    }

    private int DetectTargets()
    {
        PhysicsDebug.DrawDebug(GetStartPoint(), _stat.AttackRadius);
        return Physics.OverlapSphereNonAlloc(GetStartPoint(), _stat.AttackRadius, _hits, _layers);
    }

    private void PlaySound() => _soundContainer.Play(_stat);

    private void ResetTarget()
    {
        for (int i = 0; i < _hits.Length; i++)
            _hits[i] = null;
    }

    private Vector3 GetStartPoint() => new Vector3(transform.position.x, transform.position.y + AdditionalY, transform.position.z) + transform.forward;

    /// <summary>
    /// called from animation
    /// </summary>
    private void AttackEnded() => Hit();
}