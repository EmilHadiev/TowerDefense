using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BulletViewRender))]
public class BulletView : MonoBehaviour, IBulletView
{
    [SerializeField] private BulletViewRender _render;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _showDescriptionButton;

    private IBulletDescription _data;
    private IPlayerSoundContainer _soundContainer;

    private int _index;

    public event Action<int> Used;
    public event Action<string> Clicked;

    private void OnValidate()
    {
        _render ??= GetComponent<BulletViewRender>();
    }

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

    public void Initialize(IBulletDescription bulletData, int index, IPlayerSoundContainer soundContainer)
    {
        _data = bulletData;
        _index = index;
        _soundContainer = soundContainer;

        ShowData();
    }

    private void ShowData()
    {
        _render.Render(_data, _index);
    }

    private void OnUsed()
    {
        Used?.Invoke(_index);
        _soundContainer.Play(SoundName.SwitchBullet);
    }

    private void OnClicked() =>
        Clicked?.Invoke(_render.TranslatedDescription);
}