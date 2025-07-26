using UnityEngine;
using Zenject;

public class EvaderActivator : MonoBehaviour
{
    private LazyInject<IAttackable> _attacker;
    private LayerMask _mask;
    private readonly RaycastHit[] _hits = new RaycastHit[Constants.MaxEnemies];

    [Inject]
    private void Constructor(LazyInject<IAttackable> attackable)
    {
        _attacker = attackable;
    }

    private void Start()
    {
        _mask = LayerMask.GetMask(Constants.EnemyMask);
        _attacker.Value.Attacked += OnPlayerAttacked;
    }

    private void OnDestroy()
    {
        _attacker.Value.Attacked -= OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, _mask))
            if (hit.transform.TryGetComponent(out IEvadable evadable))
                evadable.Dodge();

        Ray ray = new Ray(transform.position, transform.forward * Mathf.Infinity);
        int targets = Physics.RaycastNonAlloc(ray, _hits, Mathf.Infinity, _mask);

        if (targets > 0)
            AttackTargets(targets);
    }

    private void AttackTargets(int targets)
    {
        for (int i = 0; i < targets; i++)
            if (_hits[i].transform.TryGetComponent(out IEvadable evadable))
                evadable.Dodge();
    }
}