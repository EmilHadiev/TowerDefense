using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class ExplosionMine : InteractiveElement
{
    [SerializeField] private TriggerObserver _observer;

    private const int Radius = 3;

    private readonly Collider[] _hits = new Collider[5];

    private PlayerStat _playerStat;
    private IPlayerSoundContainer _soundContainer;
    private IFactoryParticle _factoryParticle;
    private ParticleView _particle;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void Awake()
    {
        _particle = _factoryParticle.Create(AssetProvider.ParticleExplosionPath);
        _particle.Stop();
    }

    private void OnEnable()
    {
        _observer.Entered += OnHealthEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnHealthEntered;
        Clear();
    }

    [Inject]
    private void Constructor(IPlayerSoundContainer playerSoundContainer, IFactoryParticle factoryParticle, PlayerStat playerStat)
    {
        _playerStat = playerStat;
        _factoryParticle = factoryParticle;
        _soundContainer = playerSoundContainer;
    }

    private void OnHealthEntered(Collider collider)
    {
        Explode();
    }

    private void Explode()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, Radius, _hits, LayerMask.GetMask(Constants.PlayerMask, Constants.EnemyMask));

        if (count == 0)
            return;

        PlayView();

        for (int i = 0; i < count; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_playerStat.Damage);

        gameObject.SetActive(false);
    }

    private void PlayView()
    {
        _soundContainer.Play(SoundName.FireExplosion);

        _particle.transform.position = transform.position;
        _particle.Play();
    }

    private void Clear()
    {
        for (int i = 0; i < _hits.Length; i++)
            _hits[i] = null;
    }
}
