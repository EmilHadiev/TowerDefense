using UnityEngine;

public class DefaultPlayerMover : IMover
{
    private readonly PlayerStat _stat;
    private readonly IPlayer _player;
    private readonly IMoveHandler _moveHandler;

    private bool _isWork;

    public DefaultPlayerMover(PlayerStat stat, IPlayer player, IMoveHandler moveHandler)
    {
        _stat = stat;
        _player = player;
        _moveHandler = moveHandler;
    }

    public void StartMove() => _isWork = true;

    public void StopMove() => _isWork = false;

    public void Update()
    {
        if (_isWork == false)
            return;

        _player.Transform.Translate(_moveHandler.GetMoveDirection() * _stat.Speed * Time.deltaTime, Space.World);
    }
}