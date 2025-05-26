using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundContainer : MonoBehaviour, IPlayerSoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameSound[] _gameSounds;

    private List<BulletSound> _bulletSounds;
    private IBulletDefinition[] _bullets;

    private BulletType _bulletType;
    private bool _isReseted;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    [Inject]
    private void Constructor(IBulletDefinition[] bullets)
    {
        _bullets = bullets;
    }

    private void Awake()
    {
        _bulletSounds = new List<BulletSound>(_bullets.Length);

        foreach (var bullet in _bullets)
            _bulletSounds.Add(new BulletSound(bullet.Type, bullet.BulletData.Clip));
    }

    public void Stop() => _audioSource.Stop();

    public void Play(BulletType bulletType)
    {
        if (_bulletType == bulletType && _isReseted == false)
        {
            Play();
            return;
        }

        _bulletType = bulletType;
        _isReseted = false;
        SetClip(_bulletType);
        Play();
    }

    public void Play(string soundType)
    {
        _isReseted = true;
        SetClip(soundType);
        Play();
    }

    private void SetClip(BulletType bulletType)
    {
        AudioClip clip = _bulletSounds.FirstOrDefault(sound => sound.BulletType == bulletType).Clip;

        if (clip == null)
            throw new ArgumentNullException(nameof(bulletType));

        _audioSource.clip = clip;
    }
  
    private void SetClip(string soundName)
    {
        AudioClip clip = _gameSounds.FirstOrDefault(sound => sound.Name == soundName).Clip;

        if (clip == null)
            throw new ArgumentNullException(soundName);

        _audioSource.clip = clip;
    }

    private void Play() => _audioSource.Play();
}