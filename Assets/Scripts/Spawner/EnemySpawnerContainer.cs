using System.Collections;
using UnityEngine;

public class EnemySpawnerContainer : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;

    private WaitForSeconds _delay;
    private int _index = 0;

    private void Start()
    {
        _delay = new WaitForSeconds(Constants.EnemySpawnDelay);
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return _delay;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_index >= _spawners.Length)
            _index = 0;

        _spawners[_index].SpawnEnemy();
        _index++;
    }
}
