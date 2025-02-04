using System.Collections;
using UnityEngine;

public class EnemyAbility : MonoBehaviour
{
    private EnemyType _currentType;

    private void OnEnable()
    {
        
    }

    public void SetEnemyType(EnemyType enemyType) => _currentType = enemyType;

    public void ActivateAbility()
    {
        
    }
}