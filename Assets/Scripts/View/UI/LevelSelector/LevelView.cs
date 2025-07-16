using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color _currentLevelColor;
    [SerializeField] private Color _awardLevelColor;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _backgroundRed;
    [SerializeField] private Image _backgroundViolet;
    [SerializeField] private Image _lockerImage;
    [SerializeField] private Sprite _levelCompletedSprite;

    private Color _defaultRedColor;

    private const string CompletedText = "<sprite=3>";

    private int _currentLevel;
    private bool _isLock = false;

    public event Action<int> SelectedLevel;

    public void Initialize(LevelTracker levelTracker, int currentLevel)
    {
        _defaultRedColor = _backgroundRed.color;
        _levelText.text = (currentLevel+1).ToString();
        _currentLevel = currentLevel;
        _lockerImage.enabled = false;

        if (levelTracker.NumberLevelsCompleted == currentLevel)
            ChangeViewColor();
        else if (levelTracker.NumberLevelsCompleted > currentLevel)
            ChangeTextToCompleted();
        else
            LockLevel();
    }

    public void InitializeAward(Sprite sprite)
    {
        _backgroundViolet.sprite = sprite;
        _backgroundViolet.color = Color.white;
        _levelText.color = _awardLevelColor;
    }

    private void ChangeViewColor()
    {
        _levelText.color = _currentLevelColor;
        ChangeSelectColor();
    }

    private void ChangeTextToCompleted()
    {
        _levelText.text = CompletedText;
    }

    private void LockLevel()
    {
        _lockerImage.enabled = true;
        _isLock = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLock)
            return;

        SelectedLevel?.Invoke(_currentLevel);
        ChangeSelectColor();
    }

    public void Deselect()
    {
        _backgroundRed.color = _defaultRedColor;
    }

    private void ChangeSelectColor()
    {
        _backgroundRed.color = _currentLevelColor;
    }
}
