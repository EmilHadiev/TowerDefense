using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObstacleHealth))]
public class SecretBoxContainer : MapEventObject
{
    [SerializeField] private ObstacleHealth _health;

    private readonly List<SecretBox> _boxes = new List<SecretBox>();

    private Tween _tween;

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void Awake()
    {
        _tween = transform.DORotate(new Vector3(0, 180), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _tween.Pause();
    }

    private void OnEnable()
    {
        _health.DamageApplied += OnDamageApllied;
        Activate();
    }

    private void OnDisable()
    {
        _health.DamageApplied -= OnDamageApllied;
        _tween.Pause();
    }

    [Inject]
    private void Constructor(IPlayerSoundContainer playerSoundContainer, IEnemySoundContainer enemySound, 
        ICoinStorage coinStorage, IPlayer player, ICameraProvider cameraProvider, IFactoryParticle factoryParticle, IEnemyFactory enemyFactory)
    {
        _boxes.Add(new SecretBoxGold(playerSoundContainer, coinStorage));
        _boxes.Add(new SecretBoxHealing(enemySound, player.Health));
        _boxes.Add(new SecretBoxExplosion(enemySound, transform, player.GunPlace, cameraProvider, factoryParticle));
        _boxes.Add(new SecretBoxSpawner(enemySound, EnemyCounter, enemyFactory, coinStorage));
    }

    private void OnDamageApllied(float damage)
    {
        GetSecretBox().Activate();
        gameObject.SetActive(false);
    }

    public override void Activate()
    {
        _tween.Play();
    }

    private SecretBox GetSecretBox() 
    {
        int index = Random.Range(0, _boxes.Count);
        return _boxes[index];
    }
}