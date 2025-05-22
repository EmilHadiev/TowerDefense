using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    private IMover _mover;
    private LazyInject<IMoveHandler> _moveHandler;
    private PlayerStat _playerStat;
    private IPlayer _player;

    [Inject]
    private void Constructor(LazyInject<IMoveHandler> moveHandler, PlayerStat playerStat, IPlayer player)
    {
        _moveHandler = moveHandler;
        _playerStat = playerStat;
        _player = player;
    }

    private void Start()
    {
        _mover = new DefaultPlayerMover(_playerStat, _player, _moveHandler.Value);
        _mover.StartMove();
    }

    private void Update() => _mover.Update();
}