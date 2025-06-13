using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrainingMode : MonoBehaviour, ITrainingMode
{
    [Header("Options")]
    [SerializeField] private bool _isOn;
    [Header("UI")]
    [SerializeField] private PlayerTrainingView _playerTraining;
    [Header("Inputs")]
    [SerializeField] private LocalizedText[] _inputsText;
    [SerializeField] private RectTransform[] _inputs;
    [Header("Upgraders")]
    [SerializeField] private LocalizedText[] _upgradersText;
    [SerializeField] private RectTransform[] _upgraders;
    [Header("Elements shop")]
    [SerializeField] private LocalizedText[] _elementsText;
    [SerializeField] private RectTransform[] _elementsShops;
    [Header("Weapon show")]
    [SerializeField] private LocalizedText[] _weaponText;
    [SerializeField] private RectTransform[] _weaponShops;


    private TrainingData _trainingData;
    private Pause _pause;
    private ILevelStateSwitcher _levelSwitcher;

    private readonly List<RectTransform[]> _targets = new List<RectTransform[]>(3);

    private int _targetIndex = -1;

    [Inject]
    private void Constructor(TrainingData data, Pause pause)
    {
        _trainingData = data;
        _pause = pause;
    }

    private void Awake()
    {
        _targets.Add(_inputs);
        _targets.Add(_upgraders);
        _targets.Add(_elementsShops);
        _targets.Add(_weaponShops);
    }

    private void OnDestroy()
    {
        _playerTraining.Completed -= OnTrainingCompleted;
    }

    public bool IsStartProcess() => _trainingData.IsTrainingCompleted == false;

    public void InitTraining(ILevelStateSwitcher levelStateSwitcher)
    {
        _levelSwitcher = levelStateSwitcher;

        var template = Instantiate(_playerTraining);
        _playerTraining = template;
        _playerTraining.SetPause(_pause);
        _playerTraining.Completed += OnTrainingCompleted;
    }

    public void ShowNextTraining()
    {
        _targetIndex++;

        if (_targetIndex + 1 >= _targets.Count)
        {
            TrainingOver();
            return;
        }

        _playerTraining.SetTargets(_targets[_targetIndex]);
    }

    public void TrainingOver()
    {
        _trainingData.IsTrainingCompleted = true;
        Destroy(_playerTraining.gameObject);
        Destroy(this);
    }

    private void OnTrainingCompleted()
    {
        _levelSwitcher.SwitchState<WaitingLevelState>();
    }
}