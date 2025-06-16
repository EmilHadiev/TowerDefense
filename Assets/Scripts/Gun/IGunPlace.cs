using System;

public interface IGunPlace
{
    event Action<IGun> GunSwitched;

    IGun CurrentGun { get; }

    void SetGun(Gun gun);
}