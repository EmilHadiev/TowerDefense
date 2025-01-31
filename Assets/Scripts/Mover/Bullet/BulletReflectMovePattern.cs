using UnityEngine;

public class BulletReflectMovePattern : IBulletMover
{
    private readonly BulletData _data;
    private readonly Transform _bullet;
    private readonly BulletMover _mover;

    private bool _isWork;

    private ReflectiveObstacle _previousObstacle = null;

    public BulletReflectMovePattern(BulletData data, Transform bullet, BulletMover bulletMover)
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

        _bullet.Translate(Direction * _data.Speed * Time.deltaTime, Space.World);
    }

    private void OnCollided(Vector3 direction, ReflectiveObstacle obstacle)
    {
        if (_previousObstacle != obstacle || _previousObstacle == null)
        {
            Direction = direction;
            _previousObstacle = obstacle;
        }
    }
}