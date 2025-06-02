using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyDetector))]
public class FlamethrowerAttacker : MonoBehaviour
{
    [SerializeField] private EnemyDetector _detector;
    [SerializeField] private ParticleView _particleView;

    private const float DamagePerTime = 0.25f;

    private readonly WaitForSeconds _delay = new WaitForSeconds(DamagePerTime);

    private Coroutine _attackCoroutine;
    private PlayerStat _playerStat;

    private void OnValidate()
    {
        _detector ??= GetComponent<EnemyDetector>();
    }

    private void OnEnable()
    {
        _particleView.Stop();
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _particleView.Stop();
    }

    [Inject]
    private void Constructor(PlayerStat playerStat)
    {
        _playerStat = playerStat;
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return _delay;
            foreach (var item in _detector.GetHits())
            {
                if (item == null)
                    break;

                if (item.TryGetComponent(out IHealth health))
                    health.TakeDamage(_playerStat.Damage * DamagePerTime);
            }
        }
    }

    public void Activate()
    {
        _attackCoroutine = StartCoroutine(Attack());
        _particleView.Play();
    }
}
