using UnityEngine;
using Zenject;

public class EnemyDamageImpactSound : MonoBehaviour
{
    [SerializeField] string _damageImpactSound;

    private IEnemySoundContainer _soundContainer;
    private IDamagable _damagable;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();
    }

    private void OnEnable()
    {
        _damagable.DamageApplied += OnDamageApplied;
    }

    private void OnDisable()
    {
        _damagable.DamageApplied -= OnDamageApplied;
    }

    [Inject]
    private void Constructor(IEnemySoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    private void OnDamageApplied(float arg1)
    {
        _soundContainer.Play(_damageImpactSound);
    }
}