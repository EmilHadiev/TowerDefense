using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private const int WaitingTime = 1;

    private IInstantiator _instantiator;
    private IEnemyFactory _factory;

    private WaitForSeconds _delay;

    [Inject]
    private void Constructor(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Start()
    {
        _factory = new EnemyFactory(_instantiator);
        _delay = new WaitForSeconds(WaitingTime);

        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return _delay;
            _factory.Get(_enemy.Type);
        }
    }
}