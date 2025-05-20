using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseState : UIState
{
    [SerializeField] private Button _restartButton;

    private ISavable _savable;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Constructor(ISavable savable, ISceneLoader sceneSwitcher)
    {
        _savable = savable;
        _sceneLoader = sceneSwitcher;
    }

    public override void Enter()
    {
        base.Enter();
        _savable.SaveProgress();
    }

    protected override void RegisterToEvents()
    {
        base.RegisterToEvents();
        _restartButton.onClick.AddListener(Restart);
    }

    protected override void UnRegisterFromEvents()
    {
        base.UnRegisterFromEvents();
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        _sceneLoader.Restart();
        GameToggle.Continue();
    }
}