using System;
using UnityEngine;

public class UIStateContainer : MonoBehaviour
{
    [SerializeField] private UIState[] _states;

    private void OnValidate()
    {
        _states ??= GetComponentsInChildren<UIState>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _states.Length; i++)
            _states[i].Entered += OnEntered;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _states.Length; i++)
            _states[i].Entered -= OnEntered;
    }

    private void OnEntered(UIState uiState)
    {
        bool isContains = false;

        for (int i = 0; i < _states.Length; i++)
        {
            if (_states[i] != uiState)
                _states[i].Exit();
            else
                isContains = true;
        }

        if (isContains == false)
            throw new ArgumentOutOfRangeException(nameof(uiState));
    }
}