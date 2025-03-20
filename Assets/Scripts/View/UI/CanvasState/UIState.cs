using System;
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

    private IInput _input;

    protected Pause Pause;

    public event Action<UIState> Entered;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void OnEnable() => Enable();

    private void OnDisable() => Disable();

    [Inject]
    private void Constructor(Pause pause, IInput input)
    {
        Pause = pause;
        _input = input;
    }

    public virtual void Enter()
    {        
        Entered?.Invoke(this);
        _canvas.enabled = true;
        _input.Stop();
        Pause.Stop();
    }

    public virtual void Exit()
    {
        _canvas.enabled = false;
        _input.Continue();
        Pause.Start();
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