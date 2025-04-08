using UnityEngine;

public class PlayerConstructor : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private SphereCollider _collider;

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    private void Awake()
    {
        Destroy(this);
    }

    [ContextMenu(nameof(ConfigureComponents))]
    private void ConfigureComponents()
    {
        ConfigureRigidbody();
        ConfigureCollider();
    }

    private void ConfigureRigidbody()
    {
        _rigidBody.isKinematic = true;
    }

    private void ConfigureCollider()
    {
        float yPos = 0.5f;
        int defaultRadius = 1;

        _collider.center = new Vector3(0, yPos, 0);
        _collider.radius = defaultRadius;
    }
}