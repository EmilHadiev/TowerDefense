using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerObserver : MonoBehaviour
{
    public event Action<Collider> Entered;
    public event Action<Collider> Exited;

    private bool _isOn;

    private void OnTriggerEnter(Collider other)
    {
        if (_isOn == false)
        {
            _isOn = true;
            Entered?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isOn)
        {
            _isOn = false;
            Exited?.Invoke(other);
        }
    }
}