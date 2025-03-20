using UnityEngine;

class DesktopPlayerRotator : PlayerRotator
{
    private Vector3 _smoothDirection;
    private Vector3 _smoothVelocity;

    public DesktopPlayerRotator(IPlayer player) : base(player)
    {
    }

    public override void Rotate(Vector3 lookAtPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(lookAtPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = GetTargetPosition(hit);
            Quaternion targetRotation = GetTargetDirection(targetPosition);

            Player.Transform.rotation = Quaternion.Slerp(Player.Transform.rotation, targetRotation, Time.deltaTime * Constants.SpeedRotation);
        }
    }

    private Quaternion GetTargetDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - Player.Transform.position).normalized;
        direction.y = 0;
        _smoothDirection = Vector3.SmoothDamp(_smoothDirection, direction, ref _smoothVelocity, Constants.SmoothTime);
        Quaternion targetRotation = Quaternion.LookRotation(_smoothDirection);
        return targetRotation;
    }

    private Vector3 GetTargetPosition(RaycastHit hit)
    {
        Vector3 targetPosition = hit.point;
        targetPosition.y = Player.Transform.position.y;
        return targetPosition;
    }
}