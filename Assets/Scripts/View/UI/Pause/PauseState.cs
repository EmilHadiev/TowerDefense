using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseState : UIState
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _rewardContinueButton;
    [SerializeField] private GameObject _rewardContainer;

    private ISavable _savable;
    private ISceneLoader _sceneLoader;
    private IGameOver _gameOver;
    private IAdvertising _advertising;
    private IPlayer _player;

    [Inject]
    private void Constructor(ISavable savable, ISceneLoader sceneSwitcher, IGameOver gameOver, IAdvertising advertising, IPlayer player)
    {
        _savable = savable;
        _sceneLoader = sceneSwitcher;
        _advertising = advertising;
        _gameOver = gameOver;
        _player = player;
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
        _rewardContinueButton.onClick.AddListener(ResurrectPlayer);
        _gameOver.GameOvered += OnPlayerDied;
    }

    protected override void UnRegisterFromEvents()
    {
        base.UnRegisterFromEvents();
        _restartButton.onClick.RemoveListener(Restart);
        _rewardContinueButton.onClick.AddListener(ResurrectPlayer);
        _gameOver.GameOvered -= OnPlayerDied;
    }

    private void Restart()
    {
        _sceneLoader.Restart();
        GameToggle.Continue();
    }

    private void OnPlayerDied()
    {        
        _rewardContinueButton.gameObject.SetActive(true);
        _rewardContainer.gameObject.SetActive(true);
        Enter();
    }

    private void ResurrectPlayer()
    {
        _advertising.ShowRewardAdv(AdvType.Resurrect, callBack: _player.Resurrectable.Resurrect);
        _rewardContinueButton.gameObject.SetActive(false);
        _rewardContainer.gameObject.SetActive(false);
        Exit();
    }
}