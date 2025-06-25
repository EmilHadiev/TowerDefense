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

    private Dictionary<BulletType, AudioClip> _bulletSoundMap;
    private Dictionary<string, AudioClip> _gameSoundMap;
    private BulletType _currentBulletType;
    private bool _isReseted;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    [Inject]
    private void Constructor(IBulletDefinition[] bullets)
    {
        _bulletSoundMap = new Dictionary<BulletType, AudioClip>(bullets.Length);
        _gameSoundMap = new Dictionary<string, AudioClip>(_gameSounds.Length);

        foreach (var bullet in bullets)
        {
            _bulletSoundMap[bullet.Type] = bullet.BulletData.Clip;
        }

        foreach (var sound in _gameSounds)
        {
            _gameSoundMap[sound.Name] = sound.Clip;
        }
    }

    public void Stop() => _audioSource.Stop();

    public void Play(BulletType bulletType)
    {
        if (_currentBulletType == bulletType && _isReseted == false)
        {
            PlayCurrent();
            return;
        }

        _currentBulletType = bulletType;
        _isReseted = false;
        SetBulletClip(bulletType);
        PlayCurrent();
    }

    public void Play(string soundType)
    {
        _isReseted = true;
        SetGameClip(soundType);
        PlayCurrent();
    }

    private void SetBulletClip(BulletType bulletType)
    {
        if (_bulletSoundMap.TryGetValue(bulletType, out var clip) == false)
        {
            Debug.LogError($"Bullet sound not found for type: {bulletType}");
            return;
        }
        _audioSource.clip = clip;
    }

    private void SetGameClip(string soundName)
    {
        if (_gameSoundMap.TryGetValue(soundName, out var clip) == false)
        {
            Debug.LogError($"Game sound not found: {soundName}");
            return;
        }
        _audioSource.clip = clip;
    }

    private void PlayCurrent()
    {
        if (_audioSource.clip != null)
            _audioSource.PlayOneShot(_audioSource.clip);
    }
}