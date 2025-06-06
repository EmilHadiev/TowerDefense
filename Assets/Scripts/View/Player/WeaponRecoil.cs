﻿using DG.Tweening;

public class WeaponRecoil : IWeaponRecoil
{
    private const float RecoilDistance = 0.1f;
    private const int Accelerator = 2;

    private readonly ShakingPart _shakingPart;
    private readonly PlayerStat _stat;

    public WeaponRecoil(ShakingPart shakingPart, PlayerStat stat)
    {
        _shakingPart = shakingPart;
        _stat = stat;
    }

    public void PlayRecoil()
    {
        _shakingPart.transform.DOLocalMoveZ(GetEndValue(), GetDuration())
            .SetLoops(2, LoopType.Yoyo);
    }

    private float GetDuration() => _stat.BonusAttackSpeed / Accelerator;

    private float GetEndValue()
    {
        float localStartPositionZ = _shakingPart.transform.localPosition.z;
        return localStartPositionZ - RecoilDistance;
    }
}