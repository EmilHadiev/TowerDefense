using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerViewStorage : MonoBehaviour
{    
    [SerializeField] private ParticleView _attackParticle;

    private const float RecoilDistance = 0.1f;
    private const int Accelerator = 2;

    private IAttackable _attackable;
    private PlayerStat _stat;

    private void OnEnable() => _attackable.Attacked += OnAttacked;

    private void OnDisable() => _attackable.Attacked -= OnAttacked;

    private void Start() => _attackParticle.Stop();

    [Inject]
    private void Constructor(IAttackable attackable, PlayerStat stat)
    {
        _attackable = attackable;
        _stat = stat;
    }

    private void OnAttacked()
    {
        PlayParticle();
        PlayAnimations();
    }

    private void PlayParticle()
    {
        _attackParticle.Play();
    }

    private void PlayAnimations()
    {
        transform.DOMoveZ(GetStartPosition(),GetDelay())
            .SetLoops(2, LoopType.Yoyo);
    }

    private float GetDelay() => _stat.AttackSpeed / Accelerator;

    private float GetStartPosition() => transform.position.z - RecoilDistance;
}