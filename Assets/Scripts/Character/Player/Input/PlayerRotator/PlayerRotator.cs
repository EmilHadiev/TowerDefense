using UnityEngine;

abstract public class PlayerRotator
{
    protected readonly IPlayer Player;

    public PlayerRotator(IPlayer player)
    {
        Player = player;
    }

    public abstract void Rotate(Vector3 position);
}