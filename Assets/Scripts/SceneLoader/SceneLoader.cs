using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    private readonly ICoroutinePefrormer _coroutinePerformer;
    private readonly ISavable _savable;

    private Coroutine _sceneLoader;

    public SceneLoader(ICoroutinePefrormer coroutinePerformer, ISavable savable)
    {
        _coroutinePerformer = coroutinePerformer;
        _savable = savable;
    }

    public void SwitchTo(int index)
    {
        StopLoadScene();

        _sceneLoader = _coroutinePerformer.StartPerform(LoadScene(index));
    }

    public void Restart() => SwitchTo(SceneManager.GetActiveScene().buildIndex);

    private void StopLoadScene()
    {
        if (_sceneLoader != null)
            _coroutinePerformer.StopPerform(_sceneLoader);
    }

    private IEnumerator LoadScene(int id)
    {
        AsyncOperation async = async = SceneManager.LoadSceneAsync(id);

        while (async.isDone == false)
            yield return null;

        _savable.SaveProgress();
        Debug.Log("СЦЕНА ЗАГРУЗИЛАСЬ!");
    }
}