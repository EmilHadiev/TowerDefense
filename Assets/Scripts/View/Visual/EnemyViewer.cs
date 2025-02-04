using UnityEngine;

public class EnemyViewer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    private void OnValidate()
    {
        _renderer ??= GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        SetColor(GetRandomColor());        
    }

    private void SetColor(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private Color GetRandomColor() => Random.ColorHSV();
}