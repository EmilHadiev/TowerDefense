using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class WaitingLevelState : MonoBehaviour, ILevelState
{
    [SerializeField] private TMP_Text _timeText;

    private const int WaitingTime = 1;
    private readonly WaitForSeconds _delay = new WaitForSeconds(1);

    private Coroutine _waitingCoroutine;

    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;

    [Inject]
    private void Constructor(ILevelStateSwitcher levelSwitcher, ITrainingMode trainingMode)
    {
        _switcher = levelSwitcher;
        _trainingMode = trainingMode;
    }

    public void Enter()
    {
        Exit();
        EnableToggle(true);        
        _waitingCoroutine = StartCoroutine(WaitingCoroutine());
    }

    public void Exit()
    {
        if (_waitingCoroutine != null)
            StopCoroutine(_waitingCoroutine);

        EnableToggle(false);
    }

    private void EnableToggle(bool isOn) => _timeText.gameObject.SetActive(isOn);

    private IEnumerator WaitingCoroutine()
    {
        int time = WaitingTime;
        ShowCurrentTime(time);

        while (time > 0)
        {
            yield return _delay;
            time--;
            ShowCurrentTime(time);
        }

        StartSpawn();
    }

    private void StartSpawn()
    {
        if (_trainingMode.IsTrainingProcess())
            _switcher.SwitchState<EnemySpawnerContainer>();        
        else
            _switcher.SwitchState<EnemyUpgradeState>();
    }

    private void ShowCurrentTime(int time) => _timeText.text = time.ToString();
}