using UnityEngine;

public class BulletMoverPattern : IMover
{
    private readonly Transform _bulletTransform;
    private readonly BulletData _data;

    private bool _isMoving;

    public BulletMoverPattern(BulletData data, Transform transform)
    {
        _data = data;
        _bulletTransform = transform;
    }

    public void StartMove() => _isMoving = true;

    public void StopMove() => _isMoving = true;

    public void Update()
    {
        if (_isMoving == false)
            return;

        _bulletTransform.Translate(_bulletTransform.forward * _data.Speed * Time.deltaTime);
    }
}