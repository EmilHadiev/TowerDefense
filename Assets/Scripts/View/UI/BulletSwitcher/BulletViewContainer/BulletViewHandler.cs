public class BulletViewHandler : IBulletViewHandler
{
    private readonly IInputSystem _input;
    private readonly BulletDescriptionContainer _descriptionContainer;

    public BulletViewHandler(IInputSystem input, BulletDescriptionContainer descriptionContainer)
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