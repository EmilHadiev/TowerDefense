public interface IBullet
{
    BulletType Type { get; }
    IBulletData BulletData { get; }
    IBulletDescription BulletDescription { get; }
}