using UnityEngine;

public class SecretBoxSpawner : SecretBox
{
    private const int Min = 1;
    private const int Max = 4;
    private const int AdditionalCoins = 10;

    private readonly EnemyCounter _enemyCounter;
    private readonly IEnemyFactory _enemyFactory;
    private readonly ICoinStorage _coinStorage;
    private readonly EnemyType[] Enemies = { EnemyType.Skeleton, EnemyType.Dragon};

    public SecretBoxSpawner(ISoundContainer soundContainer, EnemyCounter enemyCounter, IEnemyFactory factory, ICoinStorage coinStorage) : base(soundContainer)
    {
        _enemyCounter = enemyCounter;
        _enemyFactory = factory;
        _coinStorage = coinStorage;
    }

    public override void Activate()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int randCount = Random.Range(Min, Max);

        for (int i = 0; i < randCount; i++)
        {
            AddEnemy();
            AddReward();
        }
    }

    private void AddReward()
    {
        _coinStorage.Add(AdditionalCoins);
    }

    private void AddEnemy()
    {
        _enemyCounter.Add();
        _enemyFactory.Create(GetRandomEnemy());
    }

    private EnemyType GetRandomEnemy()
    {
        int rand = Random.Range(0, Enemies.Length);
        return Enemies[rand];
    }
}