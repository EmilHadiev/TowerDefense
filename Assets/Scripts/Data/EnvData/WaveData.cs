using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEnvData/Wave", fileName = "GameEnvData")]
public class WaveData : ScriptableObject
{
    [SerializeField] private int _maxWaves;
    [SerializeField] private int[] _waitingTime;
    [SerializeField] private int[] _maxMonsters;

    private int _currentWave;

    public bool IsWaveEnded => _currentWave >= _maxWaves;

    public int MaxEnemies => _maxMonsters[_currentWave];
    public int WaitingTime => _waitingTime[_currentWave];

    private void OnValidate()
    {
        if (_maxMonsters.Max() > Constants.MaxEnemies)
            throw new ArgumentException("value greater than constant maximum value");

        if (_maxWaves > _maxMonsters.Length || _maxWaves > _waitingTime.Length)
            throw new ArgumentOutOfRangeException("incorrect array sizes");
    }

    private void Awake() => ResetValues();

    public void PrepareNextWave()
    {
        if (_currentWave < _maxWaves)
            _currentWave++;
    }

    private void ResetValues() => _currentWave = 0;
}