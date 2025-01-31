using UnityEngine;

public class BulletMoverPattern : IBulletMover
{
    private readonly Transform _bullet;
    private readonly BulletData _data;

    private bool _isMoving;

    //private Vector3 DefaultDirection => _bullet.forward;

    public Vector3 Direction { get; set; }

    //private Vector3 _tmpDirection = Vector3.zero;

    public BulletMoverPattern(BulletData bulletData, Transform bulletTransform)
    {
        _data = bulletData;
        _bullet = bulletTransform;
    }

    public void SetDirection(Vector3 direction) => Direction = direction;

    public void StartMove() => _isMoving = true;

    public void StopMove() => _isMoving = false;

    public void Update()
    {
        if (_isMoving == false)
            return;

        /*if (_tmpDirection == Vector3.zero)
            Move(DefaultDirection);
        else
            Move(_tmpDirection);*/
        Move(Direction);
    }

    private void Move(Vector3 direction) => _bullet.Translate(direction * _data.Speed * Time.deltaTime, Space.World);
}