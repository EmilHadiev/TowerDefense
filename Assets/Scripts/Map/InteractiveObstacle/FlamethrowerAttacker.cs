using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyDetector))]
public class FlamethrowerAttacker : MonoBehaviour
{
    [SerializeField] private EnemyDetector _detector;
    [SerializeField] private ParticleView _particleView;

    private const int DamageInterval = 300;
    private CancellationTokenSource _attackCts;
    private IGunPlace _gunPlace;

    private void OnValidate()
    {
        _detector ??= GetComponent<EnemyDetector>();
    }

    private void OnEnable()
    {
        _particleView.Stop();
        Activate();
    }

    private void OnDisable()
    {
        StopAttack();
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _gunPlace = player.GunPlace;
    }

    private async UniTaskVoid AttackLoop(CancellationToken ct)
    {
        try
        {
            while (ct.IsCancellationRequested == false)
            {
                await UniTask.Delay(DamageInterval, cancellationToken: ct);
                foreach (var enemy in _detector.GetHits())
                {
                    if (enemy == null) 
                        break;

                    if (enemy.TryGetComponent(out IHealth health))
                    {
                        health.TakeDamage(_gunPlace.CurrentGun.Damage);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {

        }
    }

    private void Activate()
    {
        StopAttack();
        Debug.Log("Активирую!");

        _attackCts = new CancellationTokenSource();
        AttackLoop(_attackCts.Token).Forget();

        _particleView.Play();
    }

    private void StopAttack()
    {
        _attackCts?.Cancel();
        _attackCts?.Dispose();
        _attackCts = null;

        _particleView.Stop();
    }
}
