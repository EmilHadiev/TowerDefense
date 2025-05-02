using System;
using UnityEngine;
using Zenject;

public class JoystickFactory : IJoystickFactory
{
    private readonly IInstantiator _instantiator;

    public JoystickFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Joystick CreateJoystick()
    {
        PlayerUI playerUI = GameObject.FindAnyObjectByType<PlayerUI>();

        if (playerUI.TryGetComponent(out RectTransform parent) == false)
            throw new ArgumentNullException(nameof(parent));

        Joystick joystick = _instantiator.InstantiatePrefabResourceForComponent<Joystick>(AssetPath.MobileInputPath, parent);

        return joystick;
    }
}