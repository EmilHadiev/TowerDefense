using System.Collections;
using UnityEngine;

public interface ICoroutinePefrormer
{
    Coroutine StartPerform(IEnumerator coroutine);

    void StopPerform(Coroutine coroutine);
}