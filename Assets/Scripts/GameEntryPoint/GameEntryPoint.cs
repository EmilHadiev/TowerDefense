using System.Collections;
using UnityEngine;
using YG;
using YG.Insides;
using Zenject;

public class YandexInitializer : MonoBehaviour
{
    private Coroutine _waitingCoroutine;
    private WaitForEndOfFrame _waitingFrame;

    private SceneSwitcher _switcher;
    private ISavable _savable;
    private GameplayerMarkup _markup;

    private void Awake() => YGInsides.LoadProgress();

    private void Start()
    {
        _waitingFrame = new WaitForEndOfFrame();

        StopPerform();
        _waitingCoroutine = StartCoroutine(AuthenticationCoroutine());
    }

    [Inject]
    private void Constructor(SceneSwitcher sceneSwitcher, ISavable savable, ICoinStorage coinStorage, GameplayerMarkup markup)
    {
        _switcher = sceneSwitcher;
        _savable = savable;
        _markup = markup;
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

        _savable.ResetAllSavesAndProgress();
        Debug.Log("�������� ���� ��� ������������!");

        OpenAuthDialog();
        LoadProgress();
        StartGameplay();
        SwitchToStartScene();
    }

    private void LoadProgress() => _savable.LoadProgress();

    private void OpenAuthDialog() => YG2.OpenAuthDialog();

    private void SwitchToStartScene() => _switcher.SwitchTo(Constants.StartScene);

    private void StartGameplay() => _markup.Start();
}