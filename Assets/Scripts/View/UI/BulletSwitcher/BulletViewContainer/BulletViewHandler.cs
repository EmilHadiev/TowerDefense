public class BulletViewHandler : IBulletViewHandler
{
    private readonly IInputHandler _input;
    private readonly BulletDescriptionContainer _descriptionContainer;

    public BulletViewHandler(IInputHandler input, BulletDescriptionContainer descriptionContainer)
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