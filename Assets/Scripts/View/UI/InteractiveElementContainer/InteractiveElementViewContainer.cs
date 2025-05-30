using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractiveElementViewContainer : MonoBehaviour
{
    [SerializeField] private InteractiveElementView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _buttonPurchase;
    [SerializeField] private Button _setElementButton;
    [SerializeField] private InteractiveElementData[] _data;

    private readonly List<InteractiveElementView> _views = new List<InteractiveElementView>();

    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;
    private IPlayer _player;
    private IInteractiveElementFactory _factory;

    private InteractiveElementData _currentData;
    
    private void Awake()
    {
        CreateTemplates(_template, _data);
        _setElementButton.onClick.AddListener(SetElement);
    }

    private void OnEnable()
    {
        _buttonPurchase.onClick.AddListener(PurchaseElement);

        foreach (var view in _views)
            view.Selected += OnElementSelected;
    }

    private void OnDisable()
    {
        _buttonPurchase.onClick.RemoveListener(PurchaseElement);

        foreach (var view in _views)
            view.Selected -= OnElementSelected;
    }

    private void OnDestroy() => 
        _setElementButton.onClick.AddListener(SetElement);

    private void SetElement()
    {
        if (_currentData == null)
            return;

        var prefab = _factory.Create(_currentData.Prefab);
        prefab.transform.position = _player.Transform.position;
        prefab.transform.rotation = _player.Transform.rotation;

        _currentData = null;
    }

    [Inject]
    private void Constructor(ICoinStorage coinStorage, ISoundContainer soundContainer, IPlayer player, IInteractiveElementFactory factory)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _player = player;
        _factory = factory;
    }

    private void CreateTemplates(InteractiveElementView template, InteractiveElementData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            InteractiveElementView view = Instantiate(template);
            view.Show(data[i]);
            _views.Add(view);
        }
    }

    private void PurchaseElement()
    {
        if (_currentData == null)
            return;

        if (_coinStorage.TrySpend(_currentData.Price))
            _soundContainer.Play(SoundName.SpendCoin);
    }

    private void OnElementSelected(InteractiveElementData data)
    {
        _currentData = data;

        foreach (var view in _views)
            view.HideSelectBackground();
    }
}