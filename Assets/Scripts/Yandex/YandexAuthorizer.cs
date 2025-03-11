using System.Collections;
using UnityEngine;
using YG;
using Zenject;

public class YandexAuthorizer : MonoBehaviour
{
    private const int StartScene = 1;

    private Coroutine _waitingCoroutine;
    private WaitForEndOfFrame _waitingFrame;
    private SceneSwitcher _switcher;

    private void Start()
    {
        _waitingFrame = new WaitForEndOfFrame();

        StopPerform();
        _waitingCoroutine = StartCoroutine(AuthenticationCoroutine());
    }

    [Inject]
    private void Constructor(SceneSwitcher sceneSwitcher)
    {
        _switcher = sceneSwitcher;
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

        if (YG2.isSDKEnabled)
        {
            YG2.OpenAuthDialog();
            _switcher.SwitchTo(StartScene);
            Debug.Log("SCENE IS INIT");
        }
    }
}