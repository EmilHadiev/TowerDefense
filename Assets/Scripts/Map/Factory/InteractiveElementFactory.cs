using Zenject;

public class InteractiveElementFactory : IInteractiveElementFactory
{
    private readonly IInstantiator _instantiator;

    public InteractiveElementFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public InteractiveElement Create(InteractiveElement interactiveElement)
    {        
        return _instantiator.InstantiatePrefab(interactiveElement).GetComponent<InteractiveElement>();
    }
        
}