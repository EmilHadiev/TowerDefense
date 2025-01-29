public interface IMovable
{
    float Speed { get; }

    void SetMover(IMover mover);
}