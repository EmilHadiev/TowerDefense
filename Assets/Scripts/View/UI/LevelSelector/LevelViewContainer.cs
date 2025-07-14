using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelViewContainer : MonoBehaviour
{
    [SerializeField] private LevelView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private ScriptableObject[] _loots;

    private readonly List<ILootable> _lootables = new List<ILootable>();

    private LevelTracker _levelTracker;

    private void OnValidate()
    {
        for (int i = 0; i < _loots.Length; i++)
        {
            if (CheckLootable(_loots[i]) == false)
                break;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < _loots.Length; i++)
        {
            if (CheckLootable(_loots[i]) == false)
                break;

            _lootables.Add(_loots[i] as ILootable);
        }

        CreateTemplates();
    }

    [Inject]
    private void Constructor(LevelTracker levelTracker)
    {
        _levelTracker = levelTracker;
    }

    private bool CheckLootable(ScriptableObject so)
    {
        if (so is ILootable == false)
        {
            so = null;
            Debug.LogError($"{so.name} does not contain interface ILootable!!!");
            return false;
        }

        return true;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < 60; i++)
        {
            LevelView view = Instantiate(_template, _container);
            view.Initialize(_levelTracker, i);
        }
    }
}