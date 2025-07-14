using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Color _currentLevelColor;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _backgroundViolet;
    [SerializeField] private Sprite _levelCompletedSprite;

    private LevelTracker _levelTracker;

    public void Initialize(LevelTracker levelTracker, int currentLevel)
    {
        _levelTracker = levelTracker;
        _levelText.text = currentLevel.ToString();

        if (_levelTracker.NumberLevelsCompleted == currentLevel)
            ChangeViewColor();
        else
            ChangeViewSprite();
    }

    private void ChangeViewSprite()
    {
        _backgroundViolet.sprite = _levelCompletedSprite;
    }

    private void ChangeViewColor()
    {
        _levelText.color = _currentLevelColor;
    }
}
