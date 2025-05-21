using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : ISceneLoader
{
    private readonly ICoroutinePefrormer _coroutinePerformer;
    private readonly ISavable _savable;

    private Coroutine _activeLoadCoroutine;

    public SceneLoader(ICoroutinePefrormer coroutinePerformer, ISavable savable)
    {
        _coroutinePerformer = coroutinePerformer;
        _savable = savable;
    }

    public void SwitchTo(int buildIndex)
    {
        StopActiveLoad();
        _activeLoadCoroutine = _coroutinePerformer.StartPerform(LoadSceneByIndex(buildIndex));
    }

    public void SwitchTo(string sceneName)
    {
        StopActiveLoad();
        _activeLoadCoroutine = _coroutinePerformer.StartPerform(LoadSceneByName(sceneName));
    }

    public void Restart() => SwitchTo(SceneManager.GetActiveScene().buildIndex);

    private void StopActiveLoad()
    {
        if (_activeLoadCoroutine != null)
        {
            _coroutinePerformer.StopPerform(_activeLoadCoroutine);
            _activeLoadCoroutine = null;
        }
    }

    private IEnumerator LoadSceneByIndex(int buildIndex)
    {
        if (!ISceneIndexValid(buildIndex))
            throw new ArgumentException(nameof(buildIndex));

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(buildIndex);
        asyncOp.allowSceneActivation = true;

        yield return asyncOp;

        if (asyncOp.isDone)
        {
            _savable?.SaveProgress();
            Debug.Log($"Scene {buildIndex} (index) loaded successfully!");
        }
    }

    private IEnumerator LoadSceneByName(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = true;

        yield return asyncOp;

        if (asyncOp.isDone)
        {
            _savable?.SaveProgress();
            Debug.Log($"Scene {sceneName} (name) loaded successfully!");
        }
    }

    private bool ISceneIndexValid(int buildIndex) =>
        buildIndex >= 0 && buildIndex<SceneManager.sceneCountInBuildSettings;
}