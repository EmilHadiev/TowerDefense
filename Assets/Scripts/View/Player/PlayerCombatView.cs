using UnityEngine;
using Zenject;

public class PlayerCombatView : MonoBehaviour
{    
    [SerializeField] private PlayerAttackParticle _attackParticle;
    [SerializeField] private ShakingPart _shakingPart;    

    private LazyInject<IAttackable> _attackable;
    private IWeaponRecoil _recoil;

    private Color _currentColor;

    private void OnValidate()
    {
        _shakingPart ??= GetComponentInChildren<ShakingPart>();
        _attackParticle ??= GetComponentInChildren<PlayerAttackParticle>();
    }

    private void Start()
    {
        _attackParticle.Stop();
        _attackable.Value.Attacked += OnAttacked;
    }

    private void OnDestroy() => _attackable.Value.Attacked -= OnAttacked;

    [Inject]
    private void Constructor(LazyInject<IAttackable> attackable, PlayerStat stat)
    {
        _attackable = attackable;
        _recoil = new WeaponRecoil(_shakingPart, stat);
    }

    public void SetParticleColor(Color color)
    {
        if (_currentColor == color)
            return;

        _currentColor = color;
        _attackParticle.SetColor(_currentColor);
    }

    private void OnAttacked() => PlayView();

    private void PlayView()
    {
        _attackParticle.Play();
        _recoil.PlayRecoil();
    }
}