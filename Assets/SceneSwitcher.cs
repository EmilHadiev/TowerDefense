using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher
{
    private readonly ICoroutinePefrormer _coroutinePerformer;

    private Coroutine _sceneLoader;

    public SceneSwitcher(ICoroutinePefrormer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    public void SwitchTo(int index)
    {
        StopLoadScene();

        _sceneLoader = _coroutinePerformer.StartPerform(SceneLoader(index));
    }

    private void StopLoadScene()
    {
        if (_sceneLoader != null)
            _coroutinePerformer.StopPerform(_sceneLoader);
    }

    private IEnumerator SceneLoader(int id)
    {
        AsyncOperation async = async = SceneManager.LoadSceneAsync(id);

        while (async.isDone == false)
        {
            yield return null;
        }

        Debug.Log("СЦЕНА ЗАГРУЗИЛАСЬ!");
    }
}