using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseState : UIState
{
    [SerializeField] private Button _restartButton;

    private ISavable _savable;
    private SceneSwitcher _switcher;

    [Inject]
    private void Constructor(ISavable savable, SceneSwitcher sceneSwitcher)
    {
        _savable = savable;
        _switcher = sceneSwitcher;
    }

    public override void Enter()
    {
        base.Enter();
        _savable.SaveProgress();
    }

    protected override void OnStart()
    {
        base.OnStart();
        _restartButton.onClick.AddListener(Restart);
    }

    protected override void Destroy()
    {
        base.Destroy();
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        _switcher.Restart();
        Pause.Start();
    }
}