using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundContainer : MonoBehaviour, ISoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BulletSound[] _bulletSounds;
    [SerializeField] private GameSound[] _gameSounds;

    private BulletType _bulletType;
    private bool _isReseted;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
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

    public void Play(SoundType soundType)
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

    private void SetClip(SoundType soundType)
    {
        AudioClip clip = _gameSounds.FirstOrDefault(sound => sound.SoundType == soundType).Clip;

        if (clip == null)
            throw new ArgumentNullException(nameof(soundType));

        _audioSource.clip = clip;
    }

    private void Play() => _audioSource.Play();
}