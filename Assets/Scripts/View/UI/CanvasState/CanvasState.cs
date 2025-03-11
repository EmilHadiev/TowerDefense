using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class CanvasState : MonoBehaviour 
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

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
        _canvas.enabled = true;
        Time.timeScale = 0;
    }

    public virtual void Exit()
    {
        _canvas.enabled = false;
        Time.timeScale = 1;
    }
}