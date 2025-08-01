﻿using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private IEntryPoint _currentPoint;

    private ISceneLoader _sceneLoader;
    private ISavable _savable;
    private GameplayMarkup _markup;
    private EnvironmentData _envData;
    private LoadingScreen _loadingScreen;
    private LevelTracker _levelTracker;

    private void Start() => StartEntryPoint();

    #if UNITY_WEBGL
    [Inject]
    public void Constructor(ISceneLoader sceneLoader, ISavable savable, GameplayMarkup markup, EnvironmentData envData, LoadingScreen loadingScreen, TrainingData data, LevelTracker levelTracker)
    {
        _sceneLoader = sceneLoader;
        _savable = savable;
        _markup = markup;
        _envData = envData;
        _loadingScreen = loadingScreen;
        _levelTracker = levelTracker;
    }
    #endif

    private void StartEntryPoint()
    {
        _loadingScreen.Show();

        #if UNITY_WEBGL
            _currentPoint = new YandexGameEntryPoint(_sceneLoader, _savable, _markup, _envData, _levelTracker);
            _currentPoint.Start();
        #endif
    }
}