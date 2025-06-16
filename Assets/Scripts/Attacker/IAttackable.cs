using System;

public interface IAttackable
{
    event Action Attacked;
    public bool IsAttacking { get; }
    public float AttackSpeed { get; }
}