using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1)] private float _timeScale;

    private void Update() => Time.timeScale = _timeScale;
}
