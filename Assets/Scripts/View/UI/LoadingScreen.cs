using DG.Tweening;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _hideDelay = 2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Hide()
    {
        _canvasGroup.alpha = 1;
    }

    public void Show()
    {
        if (_canvasGroup.alpha == 0)
            return;

        _canvasGroup.DOFade(0, _hideDelay);
    }
}
