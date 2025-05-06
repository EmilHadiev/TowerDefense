using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BulletViewContainer : MonoBehaviour
{
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private BulletDescriptionContainer _descriptionContainer;

    private IBulletViewCreator _viewCreator;
    private IBulletViewHandler _viewHandler;

    private IBulletDefinition[] _bullets;
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
    private void Constructor(IBulletSwitchHandler input, IBulletDefinition[] bullets, ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _bullets = bullets;
        _viewCreator = new BulletViewCreator(coinStorage, soundContainer, _bulletViewTemplate, _container);
        _viewHandler = new BulletViewHandler(input, _descriptionContainer);
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