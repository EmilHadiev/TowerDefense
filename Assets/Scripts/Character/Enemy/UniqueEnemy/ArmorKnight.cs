public class ArmorKnight : Enemy
{
    protected override void AbilityAccept(IEnemyVisitor visitor) => visitor.Visit(this);
}