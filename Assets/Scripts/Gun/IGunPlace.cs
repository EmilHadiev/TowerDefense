public interface IGunPlace
{
    IGun CurrentGun { get; }

    void SetGun(Gun gun);
}