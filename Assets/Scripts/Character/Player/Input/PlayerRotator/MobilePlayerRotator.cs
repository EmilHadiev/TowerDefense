using UnityEngine;

public class MobilePlayerRotator : IPlayerRotator
{
    private const int RotationSpeed = 3;
    private readonly IPlayer _player;

    public MobilePlayerRotator(IPlayer player)
    {
        _player = player;
    }

    public void Rotate(Vector3 position)
    {
        Vector3 direction = position.normalized;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            _player.Transform.rotation = Quaternion.Slerp(_player.Transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }
    }
}