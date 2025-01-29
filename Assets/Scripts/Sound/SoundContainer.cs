using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sound[] _sounds;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    public void Play(SoundType soundType)
    {
        AudioClip clip = _sounds.FirstOrDefault(sound => sound.SoundType == soundType).Clip;

        if (clip == null)
            throw new ArgumentNullException(nameof(soundType));

        _audioSource.clip = clip;
    }

    public void Stop() => _audioSource.Stop();
}