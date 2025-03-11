using System.Collections;
using UnityEngine;

public class CoroutinePerformer : MonoBehaviour, ICoroutinePefrormer
{
    public Coroutine StartPerform(IEnumerator coroutine) => StartCoroutine(coroutine);

    public void StopPerform(Coroutine coroutine) => StopCoroutine(coroutine);

    private void Awake() => DontDestroyOnLoad(this);
}