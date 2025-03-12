using System;
using System.Collections;
using UnityEngine;
using YG;
using YG.Insides;
using Zenject;

public class YandexInitializer : MonoBehaviour
{
    private const int StartScene = 1;

    private Coroutine _waitingCoroutine;
    private WaitForEndOfFrame _waitingFrame;

    private SceneSwitcher _switcher;
    private ISavable _savable;

    private void Awake()
    {
        YGInsides.LoadProgress();
    }

    private void OnEnable()
    {
        YG2.onGetSDKData += OnSDKLoaded;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnSDKLoaded;
    }

    private void Start()
    {
        _waitingFrame = new WaitForEndOfFrame();

        StopPerform();
        _waitingCoroutine = StartCoroutine(AuthenticationCoroutine());
    }

    [Inject]
    private void Constructor(SceneSwitcher sceneSwitcher, ISavable savable, ICoinStorage coinStorage)
    {
        _switcher = sceneSwitcher;
        _savable = savable;
    }

    private void StopPerform()
    {
        if (_waitingCoroutine != null)
            StopCoroutine(_waitingCoroutine);
    }

    private IEnumerator AuthenticationCoroutine()
    {
        while (YG2.isSDKEnabled == false)
            yield return _waitingCoroutine;

        OpenAuthDialog();
        SwitchScene(StartScene);

        Debug.Log("SCENE IS INIT");
    }

    private void OpenAuthDialog() => YG2.OpenAuthDialog();

    private void SwitchScene(int sceneIndex) => _switcher.SwitchTo(sceneIndex);

    private void OnSDKLoaded() => _savable.LoadProgress();
}