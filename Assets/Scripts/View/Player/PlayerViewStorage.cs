using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerViewStorage : MonoBehaviour
{    
    [SerializeField] private ParticleView _attackParticle;
    [SerializeField] private ShakingPart _shakingPart;    

    private IAttackable _attackable;
    private PlayerStat _stat;
    private PlayerAnimationsView _animationView;

    private void OnValidate() => _shakingPart ??= GetComponentInChildren<ShakingPart>();

    private void OnEnable() => _attackable.Attacked += OnAttacked;

    private void OnDisable() => _attackable.Attacked -= OnAttacked;

    private void Start()
    {
        _attackParticle.Stop();
        _animationView = new PlayerAnimationsView(_shakingPart, _stat);
    }

    [Inject]
    private void Constructor(IAttackable attackable, PlayerStat stat)
    {
        _attackable = attackable;
        _stat = stat;
    }

    private void OnAttacked()
    {
        PlayParticle();
        _animationView.PlayAttack();
    }

    private void PlayParticle()
    {
        _attackParticle.Play();
    }

    
}