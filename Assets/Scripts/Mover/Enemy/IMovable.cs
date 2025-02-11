public interface IMovable
{
    SpeedProperty Speed { get; }

    void SetMover(IMover mover);
}