using System.Collections.Generic;
using UnityEngine;

public class PlayerTraining : MonoBehaviour
{
    [SerializeField] private HintArrow _arrowTemplate;
    [SerializeField] private TooltipMessage _tooltipTemplate;
    [SerializeField] private List<RectTransform> _targets;

    private HintArrow _arrow;
    private TooltipMessage _tooltip;

    private int _index = -1;

    private void Awake()
    {
        _arrow = Instantiate(_arrowTemplate, transform);
        _tooltip = Instantiate(_tooltipTemplate, transform);
    }

    private void Start()
    {
        TooltipClosed();
    }

    private void OnEnable()
    {
        _tooltip.Closed += TooltipClosed;
    }

    private void OnDisable()
    {
        _tooltip.Closed -= TooltipClosed;
    }

    private void ShowHint()
    {
        _arrow.SetTarget(_targets[_index]);
        _tooltip.ShowMessage(_index.ToString(), _arrow);
    }

    private void TooltipClosed()
    {
        if (_index + 1 >= _targets.Count)
        {
            Destroy(gameObject);
            return;
        }

        _index++;
        ShowHint();
    }
}