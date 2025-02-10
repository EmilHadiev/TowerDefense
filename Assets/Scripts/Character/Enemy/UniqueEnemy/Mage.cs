public class Mage : Enemy
{
    protected override void AbilityAccept(IEnemyVisitor visitor) => visitor.Visit(this);
}
