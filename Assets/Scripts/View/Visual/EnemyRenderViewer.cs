using UnityEngine;

public class EnemyRenderViewer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    public Color Color { get; private set; }

    private void OnValidate()
    {
        _renderer ??= GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Awake() => _propertyBlock = new MaterialPropertyBlock();

    private void OnDisable() => ChangeColor();

    private void ChangeColor()
    {
        Color = GetRandomColor();
        SetColor(Color);
    }

    public void SetColor(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private Color GetRandomColor() => Random.ColorHSV();
}