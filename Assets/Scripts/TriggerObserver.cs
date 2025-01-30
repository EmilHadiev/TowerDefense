using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TriggerObserver : MonoBehaviour
{
    public event Action<Collider> Entered;
    public event Action<Collider> Exited;

    private bool _isOn;

    private void OnTriggerEnter(Collider other)
    {
        if (_isOn == false)
        {
            Lock();
            Entered?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isOn)
        {
            UnLock();
            Exited?.Invoke(other);
        }
    }

    public void Lock() => _isOn = true;

    public void UnLock() => _isOn = false;
}