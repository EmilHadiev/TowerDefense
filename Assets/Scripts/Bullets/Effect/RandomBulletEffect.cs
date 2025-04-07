using System;
using UnityEngine;

public class RandomBulletEffect : IBulletEffectHandler
{
    private const BulletType CurrentType = BulletType.Chaos;

    private readonly Action<int> _setEffect;

    public RandomBulletEffect(Action<int> setEffect)
    {
        _setEffect = setEffect;
    }

    public void HandleEffect(Collider enemy)
    {
        SetRandomEffect();
        ReturnEffect();
    }

    private void SetRandomEffect()
    {
        BulletType type = GetBulletType();

        while (type == CurrentType)
            type = GetBulletType();

        int index = (int)type;
        _setEffect?.Invoke(index);
    }

    private void ReturnEffect() => _setEffect?.Invoke((int)CurrentType);

    private BulletType GetBulletType() => (BulletType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BulletType)).Length);
}