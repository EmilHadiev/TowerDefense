using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class UIState : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    protected IGameBlocker GameToggle;

    public event Action<UIState> Entered;
    public event Action Exited;

    private void Start()
    {
        RegisterToEvents();
        EnableToggle(false);
    }

    private void OnDestroy() => UnRegisterFromEvents();

    [Inject]
    private void Constructor(Pause pause, IInput input) 
    {     
        GameToggle = new GameStateBlocker(input, pause);
    }

    public virtual void Enter()
    {        
        Entered?.Invoke(this);

        ChangeGameToggle(true);
        EnableToggle(true);
    }

    public virtual void Exit()
    {
        Exited?.Invoke();

        ChangeGameToggle(false);
        EnableToggle(false);
    }

    protected virtual void RegisterToEvents()
    {
        _openButton.onClick.AddListener(Enter);
        _closeButton.onClick.AddListener(Exit);
    }

    protected virtual void UnRegisterFromEvents()
    {
        _openButton.onClick.RemoveListener(Enter);
        _closeButton.onClick.RemoveListener(Exit);
    }

    private void EnableToggle(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    private void ChangeGameToggle(bool isStateEnter)
    {
        if (isStateEnter)
            GameToggle.Stop();
        else
            GameToggle.Continue();
    }
}