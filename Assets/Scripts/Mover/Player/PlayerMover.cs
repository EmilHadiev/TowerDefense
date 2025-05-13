using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    private IMover _mover;

    [Inject]
    private void Constructor(IMoveHandler moveHandler, PlayerStat playerStat, IPlayer player)
    {
        _mover = new DefaultPlayerMover(playerStat, player, moveHandler);
    }

    private void Start() => _mover.StartMove();

    private void Update() => _mover.Update();
}