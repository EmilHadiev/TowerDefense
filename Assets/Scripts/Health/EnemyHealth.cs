using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}