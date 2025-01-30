using UnityEngine;

public class EnemyMoveState : IState
{
    private readonly IStateSwitcher _switcher;
    private readonly IMover _mover;

    public EnemyMoveState(IStateSwitcher switcher, IMover mover)
    {
        _switcher = switcher;
        _mover = mover;
    }

    public void Enter()
    {
        _mover.StartMove();
    }

    public void Exit()
    {
        _mover.StopMove();
    }
}