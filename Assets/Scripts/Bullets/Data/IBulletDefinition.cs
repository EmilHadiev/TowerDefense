public interface IBulletDefinition
{
    BulletType Type { get; }
    IBulletData BulletData { get; }
    IBulletDescription BulletDescription { get; }
}