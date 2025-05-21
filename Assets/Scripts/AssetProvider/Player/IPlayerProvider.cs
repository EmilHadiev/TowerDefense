using UnityEngine;

public interface IPlayerProvider
{
    Player Create(string path);
    void SetPosition(Vector3 position);
}