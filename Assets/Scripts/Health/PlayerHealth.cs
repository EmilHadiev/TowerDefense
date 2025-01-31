using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public void AddHealth(float healthPoints)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            gameObject.SetActive(false);
    }
}