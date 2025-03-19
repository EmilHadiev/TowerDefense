using UnityEngine;

class PlayerRotator
{
    private readonly IPlayer _player;

    private Vector3 _smoothDirection;
    private Vector3 _smoothVelocity;

    public PlayerRotator(IPlayer player)
    {
        _player = player;
    }

    public void Rotate(Vector3 lookAtPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(lookAtPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = GetTargetPosition(hit);
            Quaternion targetRotation = GetTargetDirection(targetPosition);

            _player.Transform.rotation = Quaternion.Slerp(_player.Transform.rotation, targetRotation, Time.deltaTime * Constants.SpeedRotation);
        }
    }

    private Quaternion GetTargetDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _player.Transform.position).normalized;
        direction.y = 0; // Игнорируем наклон по оси Y
        _smoothDirection = Vector3.SmoothDamp(_smoothDirection, direction, ref _smoothVelocity, Constants.SmoothTime);
        Quaternion targetRotation = Quaternion.LookRotation(_smoothDirection);
        return targetRotation;
    }

    private Vector3 GetTargetPosition(RaycastHit hit)
    {
        Vector3 targetPosition = hit.point;
        targetPosition.y = _player.Transform.position.y;
        return targetPosition;
    }
}