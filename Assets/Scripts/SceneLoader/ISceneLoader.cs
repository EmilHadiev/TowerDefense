using UnityEngine;
/*using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;*/
using UnityEngine.SceneManagement;

public interface ISceneLoader
{
    void Restart();
    void SwitchTo(int index);
    void SwitchTo(string name);
}

public class AddresableSceneLoader : ISceneLoader
{
    private readonly ISavable _savable;
    //private AsyncOperationHandle<SceneInstance> _loadScene;

    public AddresableSceneLoader(ISavable savable)
    {
        _savable = savable;
    }

    public void Restart()
    {
        string name = SceneManager.GetActiveScene().name;
        SwitchTo(name);
    }

    public void SwitchTo(int index)
    {
        string name = SceneManager.GetSceneAt(index).name;
        SwitchTo(name);
    }

    public void SwitchTo(string name)
    {
       // _loadScene = Addressables.LoadSceneAsync(name, LoadSceneMode.Single);
       // _loadScene.Completed += OnCompleted;
    }

    /*private void OnCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Сцена успешно загружена");
            _loadScene.Completed -= OnCompleted;
            Addressables.UnloadSceneAsync(_loadScene);
        }
        else
        {
            Debug.LogError("Ошибка загрузки сцены: " + handle.OperationException);
        }
    }*/
}