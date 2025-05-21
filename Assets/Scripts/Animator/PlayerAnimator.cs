using UnityEngine;
using Zenject;

public class PlayerAnimator : CharacterAnimator
{
    private IMoveHandler _moveHandler;

    [Inject]
    private void Constructor(IMoveHandler moveHandler)
    {
        _moveHandler = moveHandler;
    }

    private void Update()
    {
        Running();
    }

    private void Running()
    {
        if (_moveHandler.GetMoveDirection() != Vector3.zero)
            StartRunning();
        else
            StopRunning();
    }
}
