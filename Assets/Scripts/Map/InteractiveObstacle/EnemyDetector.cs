using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _boxSize = Vector3.one; // Размер бокса для обнаружения
    [SerializeField] private Color _gizmoColor = Color.red; // Цвет для визуализации в сцене
    [SerializeField] private Color _lineColor = Color.green; // Цвет линий к врагам

    private Collider[] _hits = new Collider[Constants.MaxEnemies];
    public int Count { get; private set; }

    private void Update()
    {
        // Обнаруживаем врагов с помощью Physics.OverlapBoxNonAlloc
        Count = Physics.OverlapBoxNonAlloc(
            transform.position,
            _boxSize / 2,
            _hits,
            transform.rotation,
            LayerMask.GetMask(Constants.EnemyMask));
    }

    public IEnumerable<Collider> GetHits()
    {
        return _hits;
    }

    private void OnDrawGizmos()
    {
        // Рисуем сам бокс
        Gizmos.color = _gizmoColor;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _boxSize);

        // Рисуем линии к обнаруженным врагам
        if (_hits != null)
        {
            Gizmos.color = _lineColor;
            for (int i = 0; i < Count; i++)
            {
                if (_hits[i] != null)
                {
                    Gizmos.DrawLine(transform.position, _hits[i].transform.position);
                }
            }
        }
    }

    private void OnRenderObject()
    {
        // Рисуем линии к врагам в игровом окне
        if (_hits != null)
        {
            GL.PushMatrix();
            GL.Begin(GL.LINES);
            GL.Color(_lineColor);

            for (int i = 0; i < Count; i++)
            {
                if (_hits[i] != null)
                {
                    GL.Vertex(transform.position);
                    GL.Vertex(_hits[i].transform.position);
                }
            }

            GL.End();
            GL.PopMatrix();
        }
    }
}
