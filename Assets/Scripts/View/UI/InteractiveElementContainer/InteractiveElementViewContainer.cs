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
    private InteractiveElementPool _pools;
    private readonly Queue<InteractiveElement> _elements = new Queue<InteractiveElement>(15);

    private ICoinStorage _coinStorage;
    private IPlayerSoundContainer _soundContainer;
    private IPlayer _player;
    private IFactoryParticle _factoryParticle;

    private InteractiveElementData _currentData;
    private ParticleView _particleView;

    private void Awake()
    {
        CreateTemplates(_template, _data);
        _setElementButton.onClick.AddListener(SetElement);
        _particleView = _factoryParticle.Create(AssetProvider.ParticleBuildPath);
        _particleView.Stop();
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


    private void PlayParticleView()
    {
        _particleView.transform.position = _player.Transform.position;
        _particleView.transform.rotation = _player.Transform.rotation;
        _particleView.Play();
    }

    [Inject]
    private void Constructor(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer, IPlayer player, IInteractiveElementFactory elementFactory, IFactoryParticle factoryParticle)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _player = player;
        _factoryParticle = factoryParticle;
        _pools = new InteractiveElementPool(15, _data, elementFactory);
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
        if (_currentData == null)
            return;

        if (_coinStorage.TrySpend(_currentData.Price))
        {
            InteractiveElement element = _pools.TryGet(_currentData.Prefab);
            _elements.Enqueue(element);
            _soundContainer.Play(SoundName.SpendCoin);
        }  
    }

    private void OnElementSelected(InteractiveElementData data)
    {
        _currentData = data;

        foreach (var view in _views)
            view.HideSelectBackground();
    }

    private void SetElement()
    {
        if (_elements.Count == 0)
            return;

        CreateInteractiveElement();
        PlayParticleView();
    }

    private void CreateInteractiveElement()
    {
        var prefab = _elements.Dequeue();
        prefab.transform.position = _player.Transform.position;
        prefab.transform.rotation = _player.Transform.rotation;
        prefab.gameObject.SetActive(true);
        _soundContainer.Play(SoundName.Building);
    }
}