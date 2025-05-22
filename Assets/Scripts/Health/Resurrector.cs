using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Resurrector : MonoBehaviour, IResurrectable
{
    private const int ImmortalDelay = 3;

    private readonly WaitForSeconds _delay = new WaitForSeconds(ImmortalDelay);

    private Coroutine _coroutine;

    private IPlayer _player;
    private PlayerStat _stat;
    private IInstantiator _instantiator;
    private ParticleView _view;

    public event Action Resurrected;

    private void Awake()
    {
        _view = _instantiator.InstantiatePrefabResourceForComponent<ParticleView>(AssetProvider.ParticleMagicShieldPath);
        _view.Stop();
        _view.transform.parent = transform;
        _view.transform.position = transform.position;
    }

    [Inject]
    private void Constructor(IPlayer player, PlayerStat stat, IInstantiator instantiator)
    {
        _player = player;
        _stat = stat;
        _instantiator = instantiator;
    }

    public void Resurrect()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _player.Health.AddHealth(_stat.MaxHealth);
        _coroutine = StartCoroutine(Resurrecting());
    }

    private IEnumerator Resurrecting()
    {
        _view.Play();
        yield return _delay;
        Resurrected?.Invoke();
    }
}