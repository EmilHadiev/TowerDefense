using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private IEntryPoint _currentPoint;

    private ISceneLoader _sceneLoader;
    private ISavable _savable;
    private ICoroutinePefrormer _performer;
    private GameplayMarkup _markup;
    private EnvironmentData _envData;

    private void Start() => StartEntryPoint();

    #if UNITY_WEBGL
    [Inject]
    public void Constructor(ISceneLoader sceneLoader, ISavable savable, GameplayMarkup markup, EnvironmentData envData, ICoroutinePefrormer performer)
    {
        _sceneLoader = sceneLoader;
        _savable = savable;
        _performer = performer;
        _markup = markup;
        _envData = envData;
    }
    #endif

    private void StartEntryPoint()
    {
        #if UNITY_WEBGL
            _currentPoint = new YandexGameEntryPoint(_sceneLoader, _savable, _markup, _envData, _performer);
            _currentPoint.Start();
        #endif
    }
}