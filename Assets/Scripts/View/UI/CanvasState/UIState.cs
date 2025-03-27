using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class UIState : MonoBehaviour 
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private IInput _input;

    protected Pause Pause;

    public event Action<UIState> Entered;
    public event Action Exited;

    private void Start()
    {
        OnStart();
        EnableToggle(false);
    }

    private void OnDestroy() => Destroy();

    [Inject]
    private void Constructor(Pause pause, IInput input)
    {
        Pause = pause;
        _input = input;
    }

    public virtual void Enter()
    {        
        Entered?.Invoke(this);
        _input.Stop();
        Pause.Stop();
        EnableToggle(true);
    }

    public virtual void Exit()
    {        
        _input.Continue();
        Pause.Start();
        EnableToggle(false);
        Exited?.Invoke();
    }

    protected virtual void OnStart()
    {
        _openButton.onClick.AddListener(Enter);
        _closeButton.onClick.AddListener(Exit);
    }

    protected virtual void Destroy()
    {
        _openButton.onClick.RemoveListener(Enter);
        _closeButton.onClick.RemoveListener(Exit);
    }

    private void EnableToggle(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}