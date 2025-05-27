using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public abstract class InteractiveObstacle : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable() => 
        _observer.Entered += OnEntered;

    private void OnDisable() => 
        _observer.Entered -= OnEntered;

    private void OnEntered(Collider collider) =>
        Activate(collider);

    protected abstract void Activate(Collider collider);
}