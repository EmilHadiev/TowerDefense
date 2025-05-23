using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using Zenject;

public class Resurrector : MonoBehaviour, IResurrectable
{
    private const int ImmortalDelay = 3;

    private readonly WaitForSeconds _delay = new WaitForSeconds(ImmortalDelay);

    private Coroutine _coroutine;

    private IPlayer _player;
    private PlayerStat _stat;
    private IFactoryParticle _factoryParticle;
    private ParticleView _view;
    private ISoundContainer _soundContainer;

    public event Action Resurrected;

    private void Awake()
    {
        _view = _factoryParticle.Create(AssetProvider.ParticleMagicShieldPath, transform);
        _view.Stop();
        _view.transform.position = transform.position;
    }

    [Inject]
    private void Constructor(IPlayer player, PlayerStat stat, IFactoryParticle instantiator, ISoundContainer soundContainer)
    {
        _player = player;
        _stat = stat;
        _factoryParticle = instantiator;
        _soundContainer = soundContainer;
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
        _soundContainer.Play(SoundName.Resurrect);
        _view.Play();
        yield return _delay;
        Resurrected?.Invoke();
    }
}