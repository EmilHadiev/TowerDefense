public interface IEnemyVisitor
{
    void Visit(Mage enemy);
    void Visit(ArmorKnight armorKnight);
    void Visit(Turtle turtle);
}