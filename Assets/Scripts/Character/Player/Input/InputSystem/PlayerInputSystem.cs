using System;
using Zenject;

class PlayerInputSystem : IPlayerInputSystem, IInitializable, IDisposable
{
    public InputSystem InputSystem { get; private set; }

    public PlayerInputSystem()
    {
        InputSystem = new InputSystem();
    }

    public void Initialize() => InputSystem.Enable();

    public void Dispose() => InputSystem.Disable();
}