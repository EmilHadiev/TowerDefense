using UnityEngine;

public class Map : MonoBehaviour
{
    [field: SerializeField] public Vector3 Position { get; private set; }

    private void OnValidate()
    {
        SetPosition();
    }

    [ContextMenu(nameof(BakePosition))]
    private void BakePosition()
    {
        SetPosition();
    }

    private void SetPosition() => Position = transform.position;
}