using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            gameObject.SetActive(false);
    }
}