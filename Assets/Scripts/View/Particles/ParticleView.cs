using System;
using UnityEngine;

public class ParticleView : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;

    private void OnValidate()
    {
        if (_particles == null)
            throw new ArgumentNullException(nameof(_particles));
    }

    /// <summary>
    /// first the Stop method will be called, 
    /// then the animation will play
    /// </summary>
    public void Play()
    {
        Stop();
        EnableToggle(true);
    }

    public void Stop() => EnableToggle(false);

    public void SetColor(Color color)
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            var main = _particles[i].main;
            main.startColor = color;
        }       
    }

    private void EnableToggle(bool isOn) => gameObject.SetActive(isOn);
}
