using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundContainer : MonoBehaviour, IEnemySoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameSound[] _gameSounds;

    private List<IEnemySound> _sounds;
    private AudioClip _currentSound;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    [Inject]
    private void Constructor(IEnumerable<IEnemySound> sounds)
    {
        _sounds = new List<IEnemySound>(sounds);
    }

    public void Play(IEnemySound sound)
    {
        if (_currentSound == sound.SoundAttack)
            return;

        _currentSound = _sounds.FirstOrDefault(s => s.SoundAttack == sound.SoundAttack).SoundAttack;
        Play();
    }

    public void Play(string abilityName)
    {
        foreach (var clip in _gameSounds)
            if (clip.Name == abilityName)
                _currentSound = clip.Clip;
        Play();
    }

    public void Stop() => 
        _audioSource.Stop();

    private void Play()
    {
        if (_currentSound != null)
        {
            _audioSource.clip = _currentSound;
            _audioSource.Play();
        }
        else
        {
            Debug.LogError(_currentSound.name + " is null");
        }        
    }
}