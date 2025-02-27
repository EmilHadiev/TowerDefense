using UnityEngine;
using Zenject;

public class PlayerViewStorage : MonoBehaviour
{    
    [SerializeField] private ParticleView _attackParticle;
    [SerializeField] private ShakingPart _shakingPart;    

    private IAttackable _attackable;
    private PlayerStat _stat;
    private PlayerAnimationsView _animationView;

    private Color _currentColor;

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

    public void SetParticleViewColor(Color color)
    {
        if (_currentColor == color)
            return;

        _currentColor = color;
        _attackParticle.SetColor(_currentColor);
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