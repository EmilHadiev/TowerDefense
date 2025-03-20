using UnityEngine;

public class MobilePlayerRotator : PlayerRotator
{
    private readonly Joystick _joystick;

    public MobilePlayerRotator(IPlayer player, Joystick joystick) : base(player)
    {
    }

    public override void Rotate(Vector3 position)
    {
        Vector3 direction = position.normalized;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            Player.Transform.rotation = Quaternion.Slerp(Player.Transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
}