using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundContainer : MonoBehaviour, IEnemySoundContainer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameSound[] _gameSounds;

    private Dictionary<AudioClip, AudioClip> _enemySoundsMap;
    private Dictionary<string, AudioClip> _abilitySoundsMap;
    private AudioClip _currentSound;

    private void Awake()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    [Inject]
    private void Constructor(IEnumerable<IEnemySound> sounds)
    {
        _enemySoundsMap = new Dictionary<AudioClip, AudioClip>();
        foreach (var sound in sounds)
        {
            _enemySoundsMap[sound.SoundAttack] = sound.SoundAttack;
        }

        _abilitySoundsMap = new Dictionary<string, AudioClip>(_gameSounds.Length);
        foreach (var sound in _gameSounds)
        {
            _abilitySoundsMap[sound.Name] = sound.Clip;
        }
    }

    public void Play(IEnemySound sound)
    {
        // ќптимизаци€ 3: ”брана лишн€€ проверка и поиск
        if (_currentSound != sound.SoundAttack)
        {
            _currentSound = sound.SoundAttack;
            Debug.Log("”станавливаю звук атаки");
        }
        PlayInternal();
    }

    public void Play(string abilityName)
    {
        if (_abilitySoundsMap.TryGetValue(abilityName, out var clip))
        {
            _currentSound = clip;
            PlayInternal();
        }
        else
        {
            Debug.LogError($"Sound for ability {abilityName} not found");
        }
    }

    public void Stop() => _audioSource.Stop();

    private void PlayInternal()
    {
        if (_currentSound == null)
        {
            Debug.LogError("Current sound is null");
            return;
        }

        _audioSource.PlayOneShot(_currentSound);
    }
}