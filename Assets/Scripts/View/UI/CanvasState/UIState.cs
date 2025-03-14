using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class UIState : MonoBehaviour 
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private GameplayerMarkup _markup;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void OnEnable() => Enable();

    private void OnDisable() => Disable();

    [Inject]
    private void Constructor(GameplayerMarkup markup)
    {
        _markup = markup;
    }

    public virtual void Enter()
    {
        _canvas.enabled = true;
        Time.timeScale = 0;
        _markup.Stop();
    }

    public virtual void Exit()
    {
        _canvas.enabled = false;
        Time.timeScale = 1;
        _markup.Start();
    }

    protected virtual void Enable()
    {
        _openButton.onClick.AddListener(Enter);
        _closeButton.onClick.AddListener(Exit);
    }

    protected virtual void Disable()
    {
        _openButton.onClick.RemoveListener(Enter);
        _closeButton.onClick.RemoveListener(Exit);
    }
}