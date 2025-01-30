using System;
using System.Collections.Generic;

public class EnemyStateMachine : IStateSwitcher
{
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;

    public EnemyStateMachine(IMover mover, CharacterAnimator animator)
    {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(EnemyAttackState)] = new EnemyAttackState(this),
            [typeof(EnemyMoveState)] = new EnemyMoveState(this, mover, animator)
        };
    }

    public void SwitchTo<TState>() where TState : IState
    {
        _activeState?.Exit();
        IState state = _states[typeof(TState)];
        _activeState = state;
        state.Enter();
    }
}