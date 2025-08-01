using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class Authenticator : MonoBehaviour
{
    [SerializeField] private TMP_Text _registerText;
    [SerializeField] private Button _registerButton;

    private ISavable _saver;
    private Pause _pause;

    private void Awake()
    {
        if (IsPlayerRegistered())
        {
            DeleteRegisterElements();
        }
        else
        {
            HideRegisterElement(false);
        }
    }

    private void DeleteRegisterElements()
    {
        Destroy(_registerButton.gameObject);
        Destroy(_registerText.gameObject);
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _registerButton.onClick.AddListener(Register);
        YG2.onGetSDKData += LoadData;
        _pause.Stoped += OnGamePaused;
        _pause.Started += OnGameContinue;
    }

    private void OnDisable()
    {
        _registerButton.onClick.RemoveListener(Register);
        YG2.onGetSDKData -= LoadData;
        _pause.Stoped -= OnGamePaused;
        _pause.Started -= OnGameContinue;
    }

    private void OnGameContinue()
    {
        HideRegisterElement(false);
    }

    private void OnGamePaused()
    {        
        TryShowRegister();
    }

    [Inject]
    private void Constructor(ISavable savable, Pause pause)
    {
        _saver = savable;
        _pause = pause;
    }

    private void Register()
    {
        YG2.OpenAuthDialog();
    }

    private bool IsPlayerRegistered() => YG2.player.auth;

    private void LoadData()
    {
        if (IsPlayerRegistered() == false)
            return;

        _saver.LoadProgress();
        DeleteRegisterElements();
    }

    private void TryShowRegister()
    {
        if (IsPlayerRegistered() == false)
        {
            HideRegisterElement(true);
        }
        else
        {
            HideRegisterElement(false);
        }
    }

    private void HideRegisterElement(bool isOn)
    {
        _registerButton.gameObject.SetActive(isOn);
        _registerText.gameObject.SetActive(isOn);
    }
}
