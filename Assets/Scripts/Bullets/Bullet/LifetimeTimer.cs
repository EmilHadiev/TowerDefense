using UnityEngine;

public class LifetimeTimer : MonoBehaviour
{
    private float _tick;
    private float _lifeTime;

    private void Update() => UpdateLifeTime();

    public void StartTimer(int lifeTime) => _lifeTime = lifeTime;

    public void ResetTimer() => _tick = 0;

    private void UpdateLifeTime()
    {
        _tick += Time.deltaTime;
        HideAfterDelay();
    }

    private void HideAfterDelay()
    {
        if (_tick >= _lifeTime)
            Hide();
    }

    private void Hide() => gameObject.SetActive(false);
}
