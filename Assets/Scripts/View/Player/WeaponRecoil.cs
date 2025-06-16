using DG.Tweening;
using Zenject;

public class WeaponRecoil : IWeaponRecoil
{
    private const float RecoilDistance = 0.1f;
    private const int Accelerator = 2;

    private readonly ShakingPart _shakingPart;
    private readonly LazyInject<IAttackable> _attackable;

    public WeaponRecoil(ShakingPart shakingPart, LazyInject<IAttackable> attackable)
    {
        _shakingPart = shakingPart;
        _attackable = attackable;
    }

    public void PlayRecoil()
    {
        _shakingPart.transform.DOLocalMoveZ(GetEndValue(), GetDuration())
            .SetLoops(2, LoopType.Yoyo);
    }

    private float GetDuration() => _attackable.Value.AttackSpeed / Accelerator;

    private float GetEndValue()
    {
        float localStartPositionZ = _shakingPart.transform.localPosition.z;
        return localStartPositionZ - RecoilDistance;
    }
}