public class ReflectDamageCoefficientCalculator
{
    public float Coefficient { get; private set; }

    public void UpCoefficient() => Coefficient += Constants.ReflectedCoefficient;

    public void ResetCoefficient() => Coefficient = 0;
}