using UnityEngine;

public class BulletSwitcherViewFactory : IBulletSwitcherViewFactory
{
    private readonly ICoinStorage _coinStorage;
    private readonly ISoundContainer _soundContainer;
    private readonly BulletSwitcherView _template;
    private readonly Transform _container;

    public BulletSwitcherViewFactory(ICoinStorage coinStorage, ISoundContainer soundContainer, BulletSwitcherView template, Transform container)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _template = template;
        _container = container;
    }

    public IBulletSwitcherView CreateView(IBulletDescription data, int index)
    {
        IBulletSwitcherView view = GameObject.Instantiate(_template, _container);
        IBulletPurchaseHandler purchaseHander = new BulletPurchaseHandler(_coinStorage, _soundContainer);

        view.Initialize(data, index, purchaseHander);
        return view;
    }
}