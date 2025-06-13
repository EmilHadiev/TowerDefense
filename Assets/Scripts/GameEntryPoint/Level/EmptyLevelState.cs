using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmptyLevelState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;

    public EmptyLevelState(LevelStateMachine levelStateMachine)
    {
        _levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }
}