using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using YG;

public class YandexGameEntryPoint : IEntryPoint
{
    private CancellationTokenSource _cts;

    private readonly EnvironmentData _envData;
    private readonly ISceneLoader _sceneLoader;
    private readonly ISavable _savable;
    private readonly GameplayMarkup _markup;
    private readonly LevelTracker _tracker;

    public YandexGameEntryPoint(ISceneLoader sceneSwitcher, ISavable savable, GameplayMarkup markup, EnvironmentData envData, LevelTracker levelTracker)
    {
        _sceneLoader = sceneSwitcher;
        _envData = envData;
        _savable = savable;
        _markup = markup;
        _tracker = levelTracker;
    }

    public void Start()
    {
        StopPerform();
        _cts = new CancellationTokenSource();
        AuthenticationProcess(_cts.Token).Forget(); // Запуск без ожидания
    }

    private void StopPerform()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    private async UniTaskVoid AuthenticationProcess(CancellationToken ct)
    {
        try
        {
            while (YG2.isSDKEnabled == false)
                await UniTask.Yield(_cts.Token);

            TryRemoveTestProgress();
            //ResetProgress();
            //Debug.Log("ВРЕМЕННО СБАРСЫВАЕМ ПРОГРЕСС!");

            HideStickyBanners();
            OpenAuthDialog();
            LoadProgress();            
            SetEnvData();
            SwitchToStartScene();
            StartGameplay();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Аутентификация отменена");
        }
    }

    private void TryRemoveTestProgress()
    {
        if (YG2.saves.compltetedLevels == 0)
            ResetProgress();
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

    private void SwitchToStartScene()
    {
        if (_tracker.NumberLevelsCompleted > 0)
            _sceneLoader.SwitchTo(AssetProvider.SceneMainMenu);
        else
            _sceneLoader.SwitchTo(AssetProvider.SceneDefaultArena);
    }

    private void StartGameplay() => _markup.Ready();

    private void ResetProgress()
    {
        _savable.ResetAllSavesAndProgress();
        Debug.Log("Прогресс пока что сбрасывается!");
    }
}