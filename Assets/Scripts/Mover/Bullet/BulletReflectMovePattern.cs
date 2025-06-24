using UnityEngine;

public class BulletReflectMovePattern : IBulletMover
{
    private readonly IBulletData _data;
    private readonly Transform _bullet;
    private readonly IBulletMovable _mover;

    private bool _isWork;

    private ReflectiveObstacle _previousObstacle = null;

    public BulletReflectMovePattern(IBulletData data, Transform bullet, IBulletMovable bulletMover)
    {
        _data = data;
        _bullet = bullet;
        _mover = bulletMover;
    }

    public Vector3 Direction { get; private set; }

    public void StartMove()
    {
        _isWork = true;
        _mover.Collided += OnCollided;
    }

    public void StopMove()
    {
        _isWork = false;
        _mover.Collided -= OnCollided;
    }

    public void Update()
    {
        if (_isWork == false)
            return;
        
        UpdateBulletPosition();
    }

    private void UpdateBulletPosition()
    {
        Vector3 moveDirection = GetDirection();

        if (moveDirection == Vector3.zero)
            return;

        _bullet.Translate(moveDirection, Space.World);
        _bullet.forward = moveDirection;
    }

    private void OnCollided(Vector3 direction, ReflectiveObstacle obstacle)
    {
        if (_previousObstacle != obstacle || _previousObstacle == null)
        {
            Direction = direction;
            _previousObstacle = obstacle;
        }
    }

    private Vector3 GetDirection() => Direction * _data.Speed * Time.deltaTime;
}