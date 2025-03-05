public interface IMovable
{
    Property Speed { get; }

    void SetMover(IMover mover);
}