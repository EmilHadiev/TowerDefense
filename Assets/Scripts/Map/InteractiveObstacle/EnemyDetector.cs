using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _boxSize = Vector3.one; // Размер бокса для обнаружения
    [SerializeField] private Color _gizmoColor = Color.red; // Цвет для визуализации в сцене
    [SerializeField] private Color _lineColor = Color.green; // Цвет линий к врагам

    private Collider[] _hits = new Collider[Constants.MaxEnemies];
    private CancellationTokenSource _cts;

    public int Count { get; private set; }

    private void OnEnable()
    {
        _cts = new CancellationTokenSource();
        DetectionLoop(_cts.Token).Forget();
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTaskVoid DetectionLoop(CancellationToken ct)
    {
        while (ct.IsCancellationRequested == false)
        {
            PerformDetection();
            await UniTask.Delay(100, cancellationToken: ct);
        }
    }

    private void PerformDetection()
    {
        Count = Physics.OverlapBoxNonAlloc(
            transform.position,
            _boxSize / 2,
            _hits,
            transform.rotation,
            LayerMask.GetMask(Constants.EnemyMask));

        if (Count == 0)
            Clear();
    }

    private void Clear()
    {
        for (int i = 0; i < _hits.Length; i++)
            _hits[i] = null;
    }

    public IEnumerable<Collider> GetHits()
    {
        return _hits;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _boxSize);

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
