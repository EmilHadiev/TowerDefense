using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;

    private IPlayer _player;
    private ICoinStorage _coinStorage;
    private IMover _mover;

    private void Start()
    {
        _mover = new EnemyMovePattern(_player, _speed, transform);
        SetMover(_mover);
    }

    private void Update() => _mover.Update();

    [Inject]
    private void Constructor(IPlayer player, ICoinStorage coinStorage)
    {
        _player = player;
        _coinStorage = coinStorage;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
            _coinStorage.Add(1);
        }
    }

    public void SetMover(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}