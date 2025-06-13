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

    [Inject]
    private void Constructor(ILevelStateSwitcher levelSwitcher)
    {
        _switcher = levelSwitcher;
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

    private void StartSpawn() => _switcher.SwitchState<EnemyUpgradeState>();

    private void ShowCurrentTime(int time) => _timeText.text = time.ToString();
}