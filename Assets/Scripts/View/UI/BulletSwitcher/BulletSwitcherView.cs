using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletSwitcherView : MonoBehaviour
{
    [SerializeField] private Image _bulletImage;
    [SerializeField] private TMP_Text _bulletNameText;
    [SerializeField] private TMP_Text _bulletDescriptionText;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _showDescriptionButton;

    private BulletData _data;

    private int _index;

    public event Action<int> Used;
    public event Action<string> Clicked;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUsed);
        _showDescriptionButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUsed);
        _showDescriptionButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(BulletData bulletData, int index)
    {
        _data = bulletData;
        _index = index;
        ShowData();
    }

    private void ShowData()
    {
        _bulletImage.sprite = _data.Sprite;
        _bulletDescriptionText.text = _data.Description;
        _bulletNameText.text = _data.Name;
    }

    private void OnUsed() => Used?.Invoke(_index);

    private void OnClicked() => Clicked?.Invoke(_data.FullDescription);
}