public class BulletViewHandler : IBulletViewHandler
{
    private readonly IBulletSwitchHandler _input;
    private readonly ShopItemDescriptionContainer _descriptionContainer;

    public BulletViewHandler(IBulletSwitchHandler input, ShopItemDescriptionContainer descriptionContainer)
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