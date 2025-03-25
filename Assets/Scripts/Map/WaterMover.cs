using DG.Tweening;
using UnityEngine;

public class WaterMover : MonoBehaviour
{
    [SerializeField] private float _waveHeight = 1;

    private void Start()
    {
        transform.DOScaleY(_waveHeight, _waveHeight).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}