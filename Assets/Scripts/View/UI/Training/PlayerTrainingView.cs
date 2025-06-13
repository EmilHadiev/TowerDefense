using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrainingView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private HintArrow _arrow;
    [SerializeField] private TooltipMessage _tooltip;
    [SerializeField] private List<RectTransform> _targets;

    private int _index = -1;
    private Pause _pause;

    public event Action Completed;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
        _arrow ??= GetComponentInChildren<HintArrow>();
        _tooltip ??= GetComponentInChildren<TooltipMessage>();
    }

    private void OnEnable()
    {
        _tooltip.Closed += ShowNextTraining;        
    }

    private void OnDisable()
    {
        _tooltip.Closed -= ShowNextTraining;
    }

    public void SetPause(Pause pause)
    {
        _pause = pause;

        _pause.Started += OnGameStarted;
        _pause.Stoped += OnGamePaused;
    }

    private void OnDestroy()
    {
        _pause.Started += OnGameStarted;
        _pause.Stoped += OnGamePaused;
    }

    public void SetTargets(IEnumerable<RectTransform> targets)
    {
        UpdateTargets(targets);
        ShowNextTraining();
    }

    private void UpdateTargets(IEnumerable<RectTransform> targets)
    {
        _targets = new List<RectTransform>(targets);
        _index = -1;
    }

    private void ShowHint()
    {
        _arrow.EnableToggle(true);
        _tooltip.EnableToggle(true);

        _arrow.SetTarget(_targets[_index]);
        _tooltip.ShowMessage(_index.ToString(), _arrow);
    }

    private void ShowNextTraining()
    {
        if (_index + 1 >= _targets.Count)
        {
            _arrow.EnableToggle(false);
            _tooltip.EnableToggle(false);
            Debug.Log("Все обучения пройдены! " + nameof(PlayerTrainingView));
            Completed?.Invoke();
            return;
        }

        _index++;
        ShowHint();
    }

    private void OnGamePaused() => _canvas.enabled = false;

    private void OnGameStarted() => _canvas.enabled = true;
}