using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundContainer : MonoBehaviour, ISoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sound[] _sounds;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    public void Play(BulletType bulletType)
    {
        AudioClip clip = _sounds.FirstOrDefault(sound => sound.BulletType == bulletType).Clip;

        if (clip == null)
            throw new ArgumentNullException(nameof(bulletType));

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void Stop() => _audioSource.Stop();
}