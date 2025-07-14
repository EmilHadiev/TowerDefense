using UnityEngine;
using UnityEngine.UI;

public class GridSizeCalculator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _minCellSize = 100; // Минимальный размер клетки
    [SerializeField] private int _maxCellSize = 300; // Максимальный размер (опционально)
    [SerializeField] private bool _squareCells = true; // Делать клетки квадратными?

    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private RectTransform _rectTransform;

    private void OnValidate()
    {
        _grid ??= GetComponent<GridLayoutGroup>();
        _rectTransform ??= GetComponent<RectTransform>();
    }

    private void OnRectTransformDimensionsChange()
    {
        // Можно оптимизировать, вызывая только при изменении размера
        UpdateCellSize();
    }

    private void UpdateCellSize()
    {
        // Получаем ширину контейнера (за вычетом padding)
        float containerWidth = _rectTransform.rect.width - _grid.padding.horizontal;
        float containerHeight = _rectTransform.rect.height - _grid.padding.vertical;

        // Расчёт количества столбцов/строк (зависит от Constraint)
        int cellCount = _grid.constraintCount;
        if (_grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            // Вертикальный скролл → считаем по столбцам
            float totalSpacing = (cellCount - 1) * _grid.spacing.x;
            float cellWidth = (containerWidth - totalSpacing) / cellCount;
            cellWidth = Mathf.Clamp(cellWidth, _minCellSize, _maxCellSize);

            // Если нужно квадратное соотношение
            float cellHeight = _squareCells ? cellWidth : _grid.cellSize.y;
            _grid.cellSize = new Vector2(cellWidth, cellHeight);
        }
        else if (_grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            // Горизонтальный скролл → считаем по строкам
            float totalSpacing = (cellCount - 1) * _grid.spacing.y;
            float cellHeight = (containerHeight - totalSpacing) / cellCount;
            cellHeight = Mathf.Clamp(cellHeight, _minCellSize, _maxCellSize);

            // Если нужно квадратное соотношение
            float cellWidth = _squareCells ? cellHeight : _grid.cellSize.x;
            _grid.cellSize = new Vector2(cellWidth, cellHeight);
        }
    }
}
