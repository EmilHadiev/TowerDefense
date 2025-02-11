public interface INegativeEffectContainer
{
    void Activate<T>() where T : INegativeEffect;
}