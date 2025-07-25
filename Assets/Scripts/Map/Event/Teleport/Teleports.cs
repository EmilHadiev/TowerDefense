using UnityEngine;
using Zenject;

public class Teleports : MapEventObject
{
    [SerializeField] private TeleportPoint[] _points;
    [SerializeField] private TeleportTrigger _trigger;
    [SerializeField] private ParticleView _teleportedParticle;

    private const int Coins = 7;

    private ICoinStorage _coinStorage;

    private void OnValidate()
    {
        _points ??= GetComponentsInChildren<TeleportPoint>();
        _trigger ??= GetComponentInChildren<TeleportTrigger>();
    }

    private void OnEnable()
    {
        _trigger.EnemyEntered += OnEnemyEntered;
    }

    private void OnDisable()
    {
        _trigger.EnemyEntered -= OnEnemyEntered;
    }

    [Inject]
    private void Constructor(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    private void OnEnemyEntered(Enemy enemy)
    {
        ActivateTeleport(enemy);
    }

    private void ActivateTeleport(Enemy enemy)
    {
        Vector3 position = GetRandomSpawnPosition();

        _teleportedParticle.Stop();
        _teleportedParticle.Play();

        enemy.transform.position = position;
        _teleportedParticle.transform.position = position + Vector3.up;

        _coinStorage.Add(Coins);
    }

    public override void Activate()
    {
        TeleportsElementsToggle(true);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = UnityEngine.Random.Range(0, _points.Length);
        return _points[randomIndex].transform.position;
    }

    private void TeleportsElementsToggle(bool isOn)
    {
        for (int i = 0; i < _points.Length; i++)
            _points[i].gameObject.SetActive(isOn);

        _trigger.gameObject.SetActive(isOn);
    }
}
