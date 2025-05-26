using System.Collections.Generic;
using UnityEngine;

public class BulletViewCreator : IBulletViewCreator
{
    private readonly ICoinStorage _coinStorage;
    private readonly IPlayerSoundContainer _soundContainer;
    private readonly BulletView _template;
    private readonly Transform _container;

    public BulletViewCreator(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer, BulletView template, Transform container)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _template = template;
        _container = container;
    }

    public IReadOnlyCollection<IBulletView> GetViews(IEnumerable<IBulletDescription> data)
    {
        int index = 0;
        List<IBulletView> views = new List<IBulletView>();

        foreach (var description in data)
        {
            IBulletView template = GameObject.Instantiate(_template, _container);
            IBulletPurchaseHandler purchaseHander = new BulletPurchaseHandler(_coinStorage, _soundContainer);

            template.Initialize(description, index, purchaseHander, _soundContainer);
            index++;
            views.Add(template);
        }

        return views;
    }
}