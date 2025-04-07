using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSwitcherContainer : MonoBehaviour
{
    [SerializeField] private BulletSwitcherView _bulletViewTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private BulletSwitcherDescriptionContainer _descriptionContainer;

    private IBullet[] _bullets;
    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;

    private List<BulletSwitcherView> _switchViews;

    private IInputSystem _input;

    private void Awake()
    {
        _switchViews = new List<BulletSwitcherView>(_bullets.Length);
        CreateTemplates();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used += OnUsed;
            _switchViews[i].Clicked += OnClicked;
        }

        _container.anchoredPosition = Vector2.zero;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used -= OnUsed;
            _switchViews[i].Clicked -= OnClicked;
        }

        _descriptionContainer.EnableToggle(false);
    }

    [Inject]
    private void Constructor(IInputSystem input, IBullet[] bullets, ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _input = input;
        _bullets = bullets;
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < _bullets.Length; i++)
            CreateTemplate(_bullets[i].BulletDescription, i);
    }

    private void CreateTemplate(IBulletDescription data, int index)
    {
        BulletSwitcherView view = Instantiate(_bulletViewTemplate, _container);
        view.Initialize(data, index, _coinStorage, _soundContainer);
        _switchViews.Add(view);
    }

    private void OnUsed(int index) => _input.SwitchTo(index);

    private void OnClicked(string fullDescription) => _descriptionContainer.SetDescription(fullDescription);
}