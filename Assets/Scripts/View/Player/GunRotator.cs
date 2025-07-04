using UnityEngine;
using Zenject;

public class GunRotator : MonoBehaviour
{
    private const int SpeedBooster = 10;
    private const float StopRotateDelay = 0.5f;

    private IAttackable _attackable;
    private IGunPlace _gunPlace;

    private bool _isAttacking;

    private float _timeAfterAttack;

    private void OnEnable() => _attackable.Attacked += OnAttacked;

    private void OnDisable() => _attackable.Attacked += OnAttacked;

    private void Update()
    {
        if (_isAttacking == false)
            return;
        
        RotateGun();
        UpdateCooldown();
    }

    [Inject]
    private void Constructor(IPlayer player, IAttackable attackable)
    {
        _gunPlace = player.GunPlace;
        _attackable = attackable;
    }

    private void RotateGun()
    {
        gameObject.transform.Rotate(Vector3.forward, GetSpeed());
    }

    private void UpdateCooldown()
    {
        if (_isAttacking == false)
            return;

        _timeAfterAttack += Time.deltaTime;

        if (_timeAfterAttack >= StopRotateDelay)
            _isAttacking = false;
    }

    private float GetSpeed() => _gunPlace.CurrentGun.AttackSpeed * SpeedBooster;

    private void OnAttacked()
    {
        _isAttacking = true;
        _timeAfterAttack = 0;
    }
}
