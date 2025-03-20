using System;

public interface IInput
{
    event Action Attacked;

    void Continue();
    void Stop();
}