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
        DestroySelf();
    }

    /// <summary>
    /// destroys the component when the application starts
    /// </summary>
    public void DestroySelf()
    {
        Destroy(this);
    }

    [ContextMenu(nameof(ConfigureComponents))]
    private void ConfigureComponents()
    {       
        foreach (var config in GetConfigurators())
            config.Configurate();
    }

    private IComponentConfigurator[] GetConfigurators()
    {
        return new IComponentConfigurator[]
        {
            new RigidBodyConfigurator(_rigidBody),
            new SphereColliderConfigurator(_collider)
        };
    }
}