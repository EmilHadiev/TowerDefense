using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TrainingMode : MonoBehaviour, ITrainingMode
{
    [Header("Options")]
    [SerializeField] private bool _isOn;
    [Header("UI")]
    [SerializeField] private PlayerTrainingView _playerTraining;
    [Header("Inputs")]
    [SerializeField] private TrainingTranslate[] _inputsText;
    [SerializeField] private RectTransform[] _inputs;
    [SerializeField] private Image[] _inputsImage;
    [Header("Upgraders")]
    [SerializeField] private TrainingTranslate[] _upgradersText;
    [SerializeField] private RectTransform[] _upgraders;
    [Header("Elements shop")]
    [SerializeField] private TrainingTranslate[] _elementsText;
    [SerializeField] private RectTransform[] _elementsShops;
    [Header("Weapon show")]
    [SerializeField] private  TrainingTranslate[] _weaponText;
    [SerializeField] private RectTransform[] _weaponShops;


    private TrainingData _trainingData;
    private Pause _pause;
    private EnvironmentData _envData;
    private ILevelStateSwitcher _levelSwitcher;

    private readonly List<RectTransform[]> _targets = new List<RectTransform[]>(3);
    private readonly List<TrainingTranslate[]> _translates = new List<TrainingTranslate[]>(3);

    private int _targetIndex = -1;

    [Inject]
    private void Constructor(TrainingData data, Pause pause, EnvironmentData envData)
    {
        _trainingData = data;
        _pause = pause;
        _envData = envData;
    }

    private void Awake()
    {
        _targets.Add(_inputs);
        _targets.Add(_upgraders);
        _targets.Add(_elementsShops);
        _targets.Add(_weaponShops);

        _translates.Add(_inputsText);
        _translates.Add(_upgradersText);
        _translates.Add(_elementsText);
        _translates.Add(_weaponText);
    }

    private void OnDestroy()
    {
        _playerTraining.Completed -= OnTrainingCompleted;
    }

    public bool IsTrainingProcess() => _trainingData.IsTrainingCompleted == false;

    public void InitTraining(ILevelStateSwitcher levelStateSwitcher)
    {
        _levelSwitcher = levelStateSwitcher;

        _playerTraining = Instantiate(_trainingData.ViewPrefab);
        _playerTraining.gameObject.SetActive(true);
        _playerTraining.SetPause(_pause);
        _playerTraining.Completed += OnTrainingCompleted;

        TryToggleDesktopInput(true);
    }

    public void ShowNextTraining()
    {
        _targetIndex++;
        
        if (_targetIndex >= _targets.Count)
        {
            TrainingOver();
            OnTrainingCompleted();
            return;
        }

        _playerTraining.SetTargets(_targets[_targetIndex], _translates[_targetIndex]);
    }

    public void TrainingOver()
    {
        _trainingData.IsTrainingCompleted = true;
        _playerTraining.gameObject.SetActive(false);
        
        Destroy(this);
    }

    private void OnTrainingCompleted()
    {
        TryToggleDesktopInput(false);
        _levelSwitcher.SwitchState<WaitingLevelState>();
    }

    #region Desktop

    private void TryToggleDesktopInput(bool isOn)
    {
        if (IsDesktop())
            for (int i = 0; i < _inputs.Length; i++)
                _inputs[i].gameObject.SetActive(isOn);
    }

    private bool IsDesktop() => _envData.IsDesktop;

    #endregion
}