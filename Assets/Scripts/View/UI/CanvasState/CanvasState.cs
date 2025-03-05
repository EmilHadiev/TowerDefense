using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class CanvasState : MonoBehaviour 
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private Canvas _canvas;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Enter);
        _closeButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Enter);
        _closeButton.onClick.RemoveListener(Exit);
    }

    public virtual void Enter()
    {
        Time.timeScale = 0;
        _canvas.enabled = true;
    }

    public virtual void Exit()
    {
        Time.timeScale = 1;
        _canvas.enabled = false;
    }
}