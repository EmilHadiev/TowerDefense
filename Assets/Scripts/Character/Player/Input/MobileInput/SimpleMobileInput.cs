/// <summary>
/// only for attack
/// </summary>
public class SimpleMobileInput : BaseMobileInput
{
    public SimpleMobileInput(IJoystickFactory joystickFactory, IPlayerRotator rotator)
        : base(joystickFactory, rotator, AssetProvider.MobileInputAttackPath)
    {
    }

    public override void Tick()
    {
        if (IsWork == false) 
            return;

        HandleTouch();
    }
}