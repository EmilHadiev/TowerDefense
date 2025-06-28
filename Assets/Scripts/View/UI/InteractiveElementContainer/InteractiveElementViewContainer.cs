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
    [SerializeField] private InteractiveElementSetterView _elementSetter;
    [SerializeField] private InteractiveElementData[] _data;

    private readonly List<InteractiveElementView> _views = new List<InteractiveElementView>();
    private InteractiveElementPool _pools;

    private InteractiveElementCreator _creator;

    private InteractiveElementData _currentData;

    private void OnValidate()
    {
        _elementSetter ??= FindObjectOfType<InteractiveElementSetterView>();
    }

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


    [Inject]
    private void Constructor(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer, IInteractiveElementFactory elementFactory, IFactoryParticle factoryParticle)
    {
        _pools = new InteractiveElementPool(15, _data, elementFactory);
        _creator = new InteractiveElementCreator(factoryParticle, _pools, soundContainer, coinStorage);
    }

    private void CreateTemplates(InteractiveElementView template, InteractiveElementData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            InteractiveElementView view = Instantiate(template, _container);
            view.Show(data[i]);
            _views.Add(view);
        }
    }

    private void PurchaseElement()
    {
        if (_creator.TryPurchaseElement(_currentData))
            _elementSetter.AddSprite(_currentData.Sprite);
    }

    private void OnElementSelected(InteractiveElementData data)
    {
        _currentData = data;

        foreach (var view in _views)
            view.HideSelectBackground();
    }

    private void SetElement()
    {
        _creator.SetElement(Vector3.zero);
        _elementSetter.ShowNextSprite();
    }
}