using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuContainer : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _selectLevelButton;
    [SerializeField] private Button _selectSkinButton;

    private ISceneLoader _sceneLoader;
    private LevelTracker _tracker;
    private LoadingScreen _loadingScreen;
    private Pause _pause;

    private void OnEnable()
    {
        _pause.Continue();
        _loadingScreen.Hide();

        _continueButton.onClick.AddListener(Continue);
        _selectLevelButton.onClick.AddListener(SelectLevel);
        _selectSkinButton.onClick.AddListener(SelectSkin);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(Continue);
        _selectLevelButton.onClick.RemoveListener(SelectLevel);
        _selectSkinButton.onClick.RemoveListener(SelectSkin);
    }

    [Inject]
    private void Constructor(ISceneLoader sceneLoader, LevelTracker levelTracker, LoadingScreen loadingScreen, Pause pause)
    {
        _sceneLoader = sceneLoader;
        _tracker = levelTracker;
        _loadingScreen = loadingScreen;
        _pause = pause;
    }

    private void Continue()
    {
        _tracker.CurrentLevel = _tracker.NumberLevelsCompleted;
        _sceneLoader.SwitchTo(AssetProvider.SceneDefaultArena);
    }

    private void SelectLevel()
    {        
        _sceneLoader.SwitchTo(AssetProvider.SceneLevelSelector);
    }

    private void SelectSkin()
    {
        _sceneLoader.SwitchTo(AssetProvider.SceneSkinSelector);
    }
}