using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractiveElementPool
{
    private const int Size = 5;

    private readonly IInteractiveElementFactory _factory;
    private readonly Dictionary<InteractiveElement, List<InteractiveElement>> _elements 
        = new Dictionary<InteractiveElement, List<InteractiveElement>>(10);

    public InteractiveElementPool(int startSize, InteractiveElementData[] data, IInteractiveElementFactory elementFactory)
    {
        _factory = elementFactory;

        foreach (var item in data)
            _elements.Add(item.Prefab, new List<InteractiveElement>(startSize));
    }

    public InteractiveElement TryGet(InteractiveElement key)
    {
        var prefab = _elements[key].FirstOrDefault(element => element.gameObject.activeInHierarchy == false);

        if (prefab == null)
        {
            Create(key);
            prefab = _elements[key].FirstOrDefault(element => element.gameObject.activeInHierarchy == false);
        }

        return prefab;
    }

    private void Create(InteractiveElement key)
    {
        for (int i = 0; i < Size; i++)
        {
            var prefab = _factory.Create(key);
            prefab.gameObject.SetActive(false);
            _elements[key].Add(prefab);
        }
    }
}