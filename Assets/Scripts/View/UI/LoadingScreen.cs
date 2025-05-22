using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _hideDelay = 2;

    public event Action Loaded;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Hide()
    {
        EnableToggle(true);
        _canvasGroup.alpha = 1;
        _slider.value = 0;
    }

    public void Show()
    {
        _slider.DOValue(1, _hideDelay / 2).OnComplete(Disable);
    }

    private void Disable()
    {
        _canvasGroup.DOFade(0, _hideDelay / 2).OnComplete(OnCompleted);
    }

    private void EnableToggle(bool isOn) => gameObject.SetActive(isOn);

    private void OnCompleted()
    {
        EnableToggle(false);
        Loaded?.Invoke();
    }
}
