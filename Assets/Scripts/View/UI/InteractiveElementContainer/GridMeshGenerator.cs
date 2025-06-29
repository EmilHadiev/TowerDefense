using DG.Tweening;
using UnityEngine;

public class GridMeshGenerator : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private int _gridSize = 10;
    [SerializeField] private float _cellSize = 1f;

    private const float Duration = 0.5f;

    private Tween _tween;

    private void Awake()
    {
        _tween = transform.DOMoveY(_endPosition.y, Duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _tween.Pause();
    }

    private void OnEnable()
    {
        _tween.Play();
    }

    private void OnDisable()
    {
        _tween.Pause();
    }

    [ContextMenu(nameof(GenerateGridMesh))]
    void GenerateGridMesh()
    {
        Mesh mesh = new Mesh();
        int vertexCount = (_gridSize + 1) * 2 * 2; // Линии по X и Z
        Vector3[] vertices = new Vector3[vertexCount];
        int[] indices = new int[vertexCount];
        Color[] colors = new Color[vertexCount];

        int idx = 0;
        idx = DrawZLines(vertices, indices, idx);
        idx = DrawXLines(vertices, indices, idx);

        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        mesh.colors = colors;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private int DrawXLines(Vector3[] vertices, int[] indices, int idx)
    {
        // Вертикальные линии (X)
        for (int x = 0; x <= _gridSize; x++)
        {
            vertices[idx] = new Vector3(x * _cellSize, 0, 0);
            vertices[idx + 1] = new Vector3(x * _cellSize, 0, _gridSize * _cellSize);
            indices[idx] = idx;
            indices[idx + 1] = idx + 1;
            idx += 2;
        }

        return idx;
    }

    private int DrawZLines(Vector3[] vertices, int[] indices, int idx)
    {
        // Горизонтальные линии (Z)
        for (int z = 0; z <= _gridSize; z++)
        {
            vertices[idx] = new Vector3(0, 0, z * _cellSize);
            vertices[idx + 1] = new Vector3(_gridSize * _cellSize, 0, z * _cellSize);
            indices[idx] = idx;
            indices[idx + 1] = idx + 1;
            idx += 2;
        }

        return idx;
    }
}