using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseState : UIState
{
    [SerializeField] private Button _restartButton;

    [Header("Pause")]
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] private GameObject _continueButton;

    [Header("RewardContinue")]
    [SerializeField] private TMP_Text _defeatText;
    [SerializeField] private Button _rewardContinueButton;
    [SerializeField] private GameObject _rewardContinueContainer;

    [Header("Victory")]
    [SerializeField] private TMP_Text _victoryText;
    [SerializeField] private TMP_Text _nextText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private Button _rewardProfitButton;
    [SerializeField] private GameObject _rewardProfitContainer;
    [SerializeField] private ProfitsView _profitsView;
    [SerializeField] private AwardViewContainer _awardView;

    private ISavable _savable;
    private ISceneLoader _sceneLoader;
    private IGameOver _gameOver;
    private IAdvertising _advertising;
    private IPlayer _player;
    private IProfitContainer _profit;
    private LevelTracker _tracker;

    [Inject]
    private void Constructor(ISavable savable, ISceneLoader sceneSwitcher, IGameOver gameOver, IAdvertising advertising, IPlayer player, IProfitContainer profit, LevelTracker tracker)
    {
        _savable = savable;
        _sceneLoader = sceneSwitcher;
        _advertising = advertising;
        _gameOver = gameOver;
        _player = player;
        _profit = profit;
        _tracker = tracker;
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
        _rewardProfitButton.onClick.AddListener(AddProfit);

        _gameOver.PlayerLost += OnPlayerDied;
        _gameOver.PlayerWon += OnPlayerWon;
    }

    protected override void UnRegisterFromEvents()
    {
        base.UnRegisterFromEvents();
        _restartButton.onClick.RemoveListener(Restart);
        _rewardContinueButton.onClick.RemoveListener(ResurrectPlayer);
        _rewardProfitButton.onClick.RemoveListener(AddProfit);

        _gameOver.PlayerLost -= OnPlayerDied;
        _gameOver.PlayerWon -= OnPlayerWon;
    }

    private void Restart()
    {
        _sceneLoader.Restart();
        GameToggle.Continue();
    }

    private void OnPlayerDied()
    {
        PlayerLostView();
        Enter();
    }

    private void OnPlayerWon()
    {
        RewardContinueToggle(false);
        Enter();

        if (_awardView.IsRewardLevel())
            ShowRewardView();
        else
            ProfitsToggle(true);
    }

    private void RewardContinueToggle(bool isOn)
    {
        TextPauseStateToggle(false, true, false);

        _rewardContinueButton.gameObject.SetActive(isOn);
        _rewardContinueContainer.gameObject.SetActive(isOn);
    }

    private void ProfitsToggle(bool isOn)
    {
        PlayerWonView();

        if (_tracker.IsNotCompletedLevel == false)
            return;

        _profitsView.gameObject.SetActive(isOn);
        _rewardProfitButton.gameObject.SetActive(isOn);
        _rewardProfitContainer.gameObject.SetActive(isOn);
    }

    private void ShowRewardView()
    {
        if (_awardView.IsRewardLevel() == false)
            return;

        PlayerWonView();
        _awardView.Show();
    }

    private void PlayerWonView()
    {
        TextPauseStateToggle(false, false, true);
        TextProfitsTextToggle(false, true);
        _continueButton.gameObject.SetActive(false);
    }

    private void PlayerLostView()
    {
        ProfitsToggle(false);
        RewardContinueToggle(true);
        TextProfitsTextToggle(true, false);
    }

    private void TextProfitsTextToggle(bool isRestartText, bool isNextText)
    {
        _restartText.gameObject.SetActive(isRestartText);
        _nextText.gameObject.SetActive(isNextText);
    }

    private void TextPauseStateToggle(bool isPause, bool isDefeat, bool isVictory)
    {
        _pauseText.gameObject.SetActive(isPause);
        _defeatText.gameObject.SetActive(isDefeat);
        _victoryText.gameObject.SetActive(isVictory);
    }

    private void AddProfit()
    {
        _advertising.ShowRewardAdv(
            () => 
            { 
                _profit.AddProfitsToPlayer();
                ProfitsToggle(false);
                _continueButton.gameObject.SetActive(false);
            });
    }

    private void ResurrectPlayer()
    {
        _advertising.ShowRewardAdv(
            () =>
            {
                _player.Resurrectable.Resurrect();            
                RewardContinueToggle(false);
                TextPauseStateToggle(true, false, false);
                Exit();
            });
    }
}