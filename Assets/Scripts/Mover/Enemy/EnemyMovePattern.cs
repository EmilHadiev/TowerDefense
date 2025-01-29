using UnityEngine;

public class EnemyMovePattern : IMover
{
    private readonly IPlayer _player;
    private readonly float _speed;
    private readonly Transform _enemy;

    private bool _isWorking;

    public EnemyMovePattern(IPlayer player, float speed, Transform enemy)
    {
        _player = player;
        _speed = speed;
        _enemy = enemy;
    }

    public void StartMove()
    {
        _isWorking = true;
    }

    public void StopMove()
    {
        _isWorking = false;
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 direction = _enemy.position - _player.Transform.position;
        _enemy.Translate(direction * _speed * Time.deltaTime);
    }
}