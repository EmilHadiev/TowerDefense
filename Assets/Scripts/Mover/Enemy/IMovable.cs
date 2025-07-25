public interface IMovable
{
    Property Speed { get; }

    void SetMover(IMover mover);
    void StartMove();
    void StopMove();
}