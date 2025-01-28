using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log("Урон нанесен!");

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}