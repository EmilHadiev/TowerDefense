using System.Collections.Generic;

public class InteractiveElementPool
{
    private const int Size = 3;

    private readonly IInteractiveElementFactory _factory;
    private readonly Dictionary<InteractiveElement, List<InteractiveElement>> _elements 
        = new Dictionary<InteractiveElement, List<InteractiveElement>>(10);

    public InteractiveElementPool(int startSize, InteractiveElementData[] data, IInteractiveElementFactory elementFactory)
    {
        _factory = elementFactory;

        foreach (var item in data)
            _elements.Add(item.Prefab, new List<InteractiveElement>(startSize));
    }

    public InteractiveElement Get(InteractiveElement key)
    {
        InteractiveElement prefab = GetInactiveElement(_elements[key]);

        if (prefab == null)
        {
            Create(key);
            prefab = GetInactiveElement(_elements[key]);
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

    private InteractiveElement GetInactiveElement(List<InteractiveElement> elements)
    {
        InteractiveElement element = null;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].gameObject.activeInHierarchy == false && elements[i].IsPurchased == false)
                return elements[i];

            element = elements[i];
        }

        if (element)
            element.IsPurchased = true;
        
        return null;
    }
}