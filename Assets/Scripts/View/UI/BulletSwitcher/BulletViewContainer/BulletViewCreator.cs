using System.Collections.Generic;
using UnityEngine;

public class BulletViewCreator : IBulletViewCreator
{
    private readonly IPlayerSoundContainer _soundContainer;
    private readonly BulletView _template;
    private readonly Transform _container;

    public BulletViewCreator(IPlayerSoundContainer soundContainer, BulletView template, Transform container)
    {
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
            if (description.IsDropped == false)
                continue;

            IBulletView template = GameObject.Instantiate(_template, _container);

            template.Initialize(description, index, _soundContainer);

            if (index == 0)
                template.BackgroundToggle(true);

            index++;
            views.Add(template);

        }

        return views;
    }
}