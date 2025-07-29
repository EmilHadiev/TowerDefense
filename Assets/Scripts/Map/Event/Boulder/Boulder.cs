using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObstacleHealth))]
public class Boulder : MapEventObject
{
    [SerializeField] private float _speed;
    [SerializeField] private ObstacleHealth _health;

    private const int Coins = 100;

    private IPlayer _player;
    private IGameOver _gameOver;
    private ICoinStorage _coinStorage;
    private IPlayerSoundContainer _soundContainer;

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    [Inject]
    private void Constructor(IPlayer player, ICoinStorage coinStorage, IPlayerSoundContainer playerSoundContainer, IGameOver gameOver)
    {
        _coinStorage = coinStorage;
        _soundContainer = playerSoundContainer;
        _player = player;
        _gameOver = gameOver;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.Transform.position, _speed * Time.deltaTime);

        if (IsCollidedWithPlayer())
            KillPlayer();
    }

    private void KillPlayer()
    {
        _gameOver.GameOver();
        gameObject.SetActive(false);
    }

    private bool IsCollidedWithPlayer()
    {
        return Vector3.Distance(transform.position, _player.Transform.position) <= 1;
    }

    public override void Activate()
    {

    }

    private void OnDied()
    {
        _soundContainer.Stop();
        _soundContainer.Play(SoundName.SpendCoin);
        _coinStorage.Add(Coins);
    }
}