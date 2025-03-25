using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSwitcherContainer : MonoBehaviour
{
    [SerializeField] private BulletSwitcherView _bulletViewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private BulletSwitcherDescriptionContainer _descriptionContainer;
    [SerializeField] private BulletData[] _data;

    private List<BulletSwitcherView> _switchViews;

    private IInputSystem _input;

    private void Awake()
    {
        _switchViews = new List<BulletSwitcherView>(_data.Length);
        CreateTemplates();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used += OnUsed;
            _switchViews[i].Clicked += OnClicked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _switchViews.Count; i++)
        {
            _switchViews[i].Used -= OnUsed;
            _switchViews[i].Clicked -= OnClicked;
        }
    }

    [Inject]
    private void Constructor(IInputSystem input)
    {
        _input = input;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < _data.Length; i++)
            CreateTemplate(_data[i], i);
    }

    private void CreateTemplate(BulletData data, int index)
    {
        BulletSwitcherView view = Instantiate(_bulletViewTemplate, _container);
        view.Initialize(data, index);
        _switchViews.Add(view);
    }

    private void OnUsed(int index) => _input.SwitchTo(index);

    private void OnClicked(string fullDescription) => _descriptionContainer.SetDescription(fullDescription);
}