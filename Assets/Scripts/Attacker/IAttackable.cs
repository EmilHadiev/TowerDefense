using System;

public interface IAttackable
{
    event Action Attacked;
    void SetAttackSound(BulletType bulletType);
}