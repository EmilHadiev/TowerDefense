public class Turtle : Enemy
{
    protected override void AbilityAccept(IEnemyVisitor visitor) => visitor.Visit(this);
}