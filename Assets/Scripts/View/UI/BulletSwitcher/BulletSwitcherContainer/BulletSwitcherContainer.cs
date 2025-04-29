using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BulletSwitcherContainer : MonoBehaviour
{
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private BulletSwitcherDescriptionContainer _descriptionContainer;

    private IBulletSwitcherViewCreator _viewCreator;
    private IBulletSwitcherViewHandler _viewHandler;

    private IBullet[] _bullets;
    private List<IBulletView> _switchViews;

    private void Awake()
    {
        _switchViews = new List<IBulletView>(_bullets.Length);
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
        _viewCreator = new BulletSwitcherViewCreator(coinStorage, soundContainer, _bulletViewTemplate, _container);
        _viewHandler = new BulletSwitcherViewHandler(input, _descriptionContainer);
    }

    private void CreateTemplates()
    {
        IBulletDescription[] data = _bullets.Select(bullet => bullet.BulletDescription).ToArray();

        foreach (var bullet in _viewCreator.GetViews(data))
            AddTemplate(bullet);
    }

    private void AddTemplate(IBulletView view) => 
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