using UnityEngine;
using Zenject;

public class PlayerAnimator : CharacterAnimator
{
    [Inject]
    private LazyInject<IMoveHandler> _moveHandler;

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
