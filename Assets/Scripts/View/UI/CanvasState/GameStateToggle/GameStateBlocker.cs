public class GameStateBlocker : IGameBlocker
{
    private readonly IInput _input;
    private readonly Pause _pause;

    public GameStateBlocker(IInput input, Pause pause)
    {
        _input = input;
        _pause = pause;
    }

    public void Stop()
    {
        _input.Stop();
        _pause.Stop();
    }

    public void Continue()
    {
        _input.Continue();
        _pause.Continue();
    }
}
