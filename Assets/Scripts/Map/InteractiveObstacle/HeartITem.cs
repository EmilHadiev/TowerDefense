using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObstacleHealth))]
[RequireComponent(typeof(Rigidbody))]
public class HeartITem : InteractiveElement
{
    [SerializeField] private ObstacleHealth _health;
    [SerializeField] private float _rotateSpeed;

    [Inject]
    private IPlayer _player;

    private const int AdditionalHealth = 1000;

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void OnEnable()
    {
        _health.DamageApplied += HealPlayer;
        transform.position += GetUpPosition();
    }

    private void OnDisable()
    {
        _health.DamageApplied -= HealPlayer;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(transform.up, _rotateSpeed * Time.deltaTime);
    }

    private void HealPlayer(float obj)
    {
        _player.Health.AddHealth(AdditionalHealth);
        gameObject.SetActive(false);
    }

    private Vector3 GetUpPosition()
    {
        return transform.up * 2;
    }
}