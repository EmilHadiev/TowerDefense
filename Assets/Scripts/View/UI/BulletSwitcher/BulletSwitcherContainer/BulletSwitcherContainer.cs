using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSwitcherContainer : MonoBehaviour
{
    [SerializeField] private BulletSwitcherView _bulletViewTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private BulletSwitcherDescriptionContainer _descriptionContainer;

    private IBulletSwitcherViewFactory _viewFactory;
    private IBulletSwitcherViewHandler _viewHandler;

    private IBullet[] _bullets;
    private List<IBulletSwitcherView> _switchViews;

    private void Awake()
    {
        _switchViews = new List<IBulletSwitcherView>(_bullets.Length);
        CreateTemplates();
    }

    private void OnEnable()
    {
        SubscribeToViewEvents();
        ResetScrollPosition();
    }

    private void OnDisable()
    {
        UnSubscribeFromViewEvents();
        DisableDescription();
    }

    [Inject]
    private void Constructor(IInputSystem input, IBullet[] bullets, ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _bullets = bullets;
        _viewFactory = new BulletSwitcherViewFactory(coinStorage, soundContainer, _bulletViewTemplate, _container);
        _viewHandler = new BulletSwitcherViewHandler(input, _descriptionContainer);
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < _bullets.Length; i++)
            AddTemplate(_viewFactory.CreateView(_bullets[i].BulletDescription, i));
    }

    private void AddTemplate(IBulletSwitcherView view) => 
        _switchViews.Add(view);

    private void SubscribeToViewEvents()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used += _viewHandler.HandleViewUsed;
            _switchViews[i].Clicked += _viewHandler.HandleViewClicked;
        }
    }

    private void UnSubscribeFromViewEvents()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used -= _viewHandler.HandleViewUsed;
            _switchViews[i].Clicked -= _viewHandler.HandleViewClicked;
        }
    }

    private void ResetScrollPosition() => 
        _container.anchoredPosition = Vector2.zero;

    private void DisableDescription() => 
        _descriptionContainer.EnableToggle(false);
}