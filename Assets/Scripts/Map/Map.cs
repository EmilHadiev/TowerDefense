using UnityEngine;

public class Map : MonoBehaviour
{
    [field: SerializeField] public Vector3 Position { get; private set; }

    private void OnValidate()
    {
        BakePosition();
    }

    [ContextMenu(nameof(SetPosition))]
    private void SetPosition()
    {
        transform.position = Position;
    }

    [ContextMenu(nameof(BakePosition))]
    private void BakePosition()
    {
        Position = transform.position;
    }
}