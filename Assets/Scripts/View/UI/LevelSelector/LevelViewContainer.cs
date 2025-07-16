using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelViewContainer : MonoBehaviour
{
    [SerializeField] private LevelView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _buttonLaunch;

    private readonly List<LevelView> _views = new List<LevelView>(60);

    private LevelTracker _levelTracker;
    private AwardGiver _awardGiver;
    private ISceneLoader _sceneLoader;

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnEnable()
    {
        foreach (var view in _views)
            view.SelectedLevel += OnSelectedLevel;

        _buttonLaunch.onClick.AddListener(LoadLevel);
    }

    private void OnDisable()
    {
        foreach (var view in _views)
            view.SelectedLevel -= OnSelectedLevel;

        _buttonLaunch.onClick.RemoveListener(LoadLevel);
    }

    [Inject]
    private void Constructor(LevelTracker levelTracker, AwardGiver awardGiver, ISceneLoader sceneLoader)
    {
        _levelTracker = levelTracker;
        _awardGiver = awardGiver;
        _sceneLoader = sceneLoader;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < 60; i++)
        {
            LevelView view = Instantiate(_template, _container);
            _views.Add(view);

            view.Initialize(_levelTracker, i);

            if (TryGetGunData(i, out GunData gunData))
                view.InitializeAward(gunData.Sprite);

            if (TryGetBulletData(i, out IBulletDefinition bulletData))
                view.InitializeAward(bulletData.BulletDescription.Sprite);
        }
    }

    private bool TryGetBulletData(int levelNumber, out IBulletDefinition bulletData)
    {
        bulletData = null;

        if (_awardGiver.Bullets.TryGetValue(levelNumber, out IBulletDefinition data))
        {
            bulletData = data;
            return true;
        }

        return false;
    }

    private bool TryGetGunData(int levelNumber, out GunData gunData)
    {
        gunData = null;

        if (_awardGiver.Guns.TryGetValue(levelNumber, out GunData data))
        {
            gunData = data;
            return true;
        }

        return false;
    }

    private void OnSelectedLevel(int level)
    {
        _levelTracker.CurrentLevel = level;

        foreach (var view in _views)
            view.Deselect();
    }

    private void LoadLevel() 
    {
        _sceneLoader.SwitchTo(AssetProvider.SceneDefaultArena);
    }
}