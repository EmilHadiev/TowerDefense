using UnityEngine;
using Zenject;

public abstract class MapEventObject : MonoBehaviour
{
    [Inject]
    protected EnemyCounter EnemyCounter;

    public abstract void Activate();
}