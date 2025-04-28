public class BulletSwitcherViewHandler : IBulletSwitcherViewHandler
{
    private readonly IInputSystem _input;
    private readonly BulletSwitcherDescriptionContainer _descriptionContainer;

    public BulletSwitcherViewHandler(IInputSystem input, BulletSwitcherDescriptionContainer descriptionContainer)
    {
        _input = input;
        _descriptionContainer = descriptionContainer;
    }

    public void HandleViewUsed(int index) => _input.SwitchTo(index);
    public void HandleViewClicked(string fullDescription)
    {
        _descriptionContainer.EnableToggle(true);
        _descriptionContainer.SetDescription(fullDescription);
    }
}

public interface IBulletSwitcherViewHandler
{
    void HandleViewClicked(string fullDescription);
    void HandleViewUsed(int index);
}