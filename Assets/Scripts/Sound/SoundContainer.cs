using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundContainer : MonoBehaviour, ISoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sound[] _sounds;

    private BulletType _bulletType;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    public void Stop() => _audioSource.Stop();

    public void Play(BulletType bulletType)
    {
        if (_bulletType == bulletType)
        {
            Play();
            return;
        }

        _bulletType = bulletType;

        SetClip(_bulletType);
        Play();
    }

    private void SetClip(BulletType bulletType)
    {
        AudioClip clip = _sounds.FirstOrDefault(sound => sound.BulletType == bulletType).Clip;

        if (clip == null)
            throw new ArgumentNullException(nameof(bulletType));

        _audioSource.clip = clip;
    }

    private void Play() => _audioSource.Play();
}