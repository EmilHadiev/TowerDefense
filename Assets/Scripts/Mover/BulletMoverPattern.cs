using UnityEngine;

public class BulletMoverPattern : IMover
{
    private readonly Transform _bullet;
    private readonly BulletData _data;

    private bool _isMoving;

    public BulletMoverPattern(BulletData bulletData, Transform bulletTransform)
    {
        _data = bulletData;
        _bullet = bulletTransform;
    }

    public void StartMove() => _isMoving = true;

    public void StopMove() => _isMoving = false;

    public void Update()
    {
        if (_isMoving == false)
            return;

        _bullet.Translate(GetDirection() * _data.Speed * Time.deltaTime, Space.World);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = (_bullet.forward).normalized;
        return direction;
    }
}