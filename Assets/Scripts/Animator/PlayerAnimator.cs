using UnityEngine;
using Zenject;

public class PlayerAnimator : CharacterAnimator
{
    private LazyInject<IMoveHandler> _moveHandler;

    [Inject]
    private void Constructor(LazyInject<IMoveHandler> moveHandler)
    {
        _moveHandler = moveHandler;
    }

    private void Update()
    {
        Running();
    }

    private void Running()
    {
        if (_moveHandler.Value.GetMoveDirection() != Vector3.zero)
            StartRunning();
        else
            StopRunning();
    }
}
