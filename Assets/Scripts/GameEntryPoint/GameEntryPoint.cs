using System.Collections;
using UnityEngine;
using YG;

public class YandexGameEntryPoint : IEntryPoint
{
    private Coroutine _waitingCoroutine;
    private readonly ICoroutinePefrormer _performer;
    private readonly WaitForEndOfFrame _waitingFrame;

    private readonly EnvironmentData _envData;
    private readonly ISceneLoader _sceneLoader;
    private readonly ISavable _savable;
    private readonly GameplayMarkup _markup;

    public YandexGameEntryPoint(ISceneLoader sceneSwitcher, ISavable savable, GameplayMarkup markup, EnvironmentData envData, ICoroutinePefrormer performer)
    {
        _waitingFrame = new WaitForEndOfFrame();
        _sceneLoader = sceneSwitcher;
        _envData = envData;
        _savable = savable;
        _performer = performer;
        _markup = markup;
    }

    public void Start()
    {
        StopPerform();
        _waitingCoroutine = _performer.StartPerform(AuthenticationCoroutine());
    }

    private void StopPerform()
    {
        if (_waitingCoroutine != null)
            _performer.StopPerform(_waitingCoroutine);
    }

    private IEnumerator AuthenticationCoroutine()
    {
        while (YG2.isSDKEnabled == false)
            yield return _waitingFrame;

        #if UNITY_EDITOR
            ResetProgress();
        #endif

        HideStickyBanners();

        OpenAuthDialog();
        LoadProgress();
        SetEnvData();
        SwitchToStartScene();
        StartGameplay();
    }

    private void SetEnvData()
    {
        if (YG2.envir.isDesktop)
            _envData.IsDesktop = true;
        else
            _envData.IsDesktop = false;

        if (YG2.envir.language == "ru")
            _envData.Language = LanguageType.ru;
        else if (YG2.envir.language == "tr")
            _envData.Language = LanguageType.tr;
        else
            _envData.Language = LanguageType.en;
    }

    private void HideStickyBanners() => YG2.StickyAdActivity(false);

    private void LoadProgress() => _savable.LoadProgress();

    private void OpenAuthDialog() => YG2.OpenAuthDialog();

    private void SwitchToStartScene() => _sceneLoader.SwitchTo(Constants.StartScene);

    private void StartGameplay() => _markup.Ready();

    private void ResetProgress()
    {
        _savable.ResetAllSavesAndProgress();
        Debug.Log("Прогресс пока что сбрасывается!");
    }
}