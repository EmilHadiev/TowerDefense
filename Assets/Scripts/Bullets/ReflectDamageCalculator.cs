public class ReflectDamageCalculator
{
    public float Damage { get; private set; }

    public void UpDamage() => Damage += Constants.ReflectedCoefficient;

    public void ResetDamage() => Damage = 0;
}
