using System;
using UnityEngine;

public interface IInput
{
    event Action Attacked;
    event Action<Vector3> Moving;

    void Continue();
    void Stop();
}