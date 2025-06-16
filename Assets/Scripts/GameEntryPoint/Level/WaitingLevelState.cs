using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class WaitingLevelState : MonoBehaviour, ILevelState
{
    [SerializeField] private TMP_Text _timeText;

    private readonly WaitForSeconds _delay = new WaitForSeconds(1);

    private Coroutine _waitingCoroutine;

    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;
    private WaveData _waveData;

    [Inject]
    private void Constructor(ILevelStateSwitcher levelSwitcher, ITrainingMode trainingMode, WaveData waveData)
    {
        _switcher = levelSwitcher;
        _trainingMode = trainingMode;
        _waveData = waveData;
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
        int time = _waveData.WaitingTime;
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