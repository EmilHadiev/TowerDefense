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
    [SerializeField] private Image _backgroundViolet;
    [SerializeField] private Image _lockerImage;
    [SerializeField] private Sprite _levelCompletedSprite;

    private const string CompletedText = "<sprite=3>";

    private int _currentLevel;

    public event Action<int> SelectedLevel;

    public void Initialize(LevelTracker levelTracker, int currentLevel)
    {
        _levelText.text = (currentLevel+1).ToString();
        _currentLevel = currentLevel;
        _lockerImage.enabled = false;

        if (levelTracker.NumberLevelsCompleted == currentLevel)
            ChangeViewColor();
        else if (levelTracker.NumberLevelsCompleted > currentLevel)
            ChangeTextToCompleted();
        else
            _lockerImage.enabled = true;
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
    }

    private void ChangeTextToCompleted()
    {
        _levelText.text = CompletedText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectedLevel?.Invoke(_currentLevel);
    }
}
