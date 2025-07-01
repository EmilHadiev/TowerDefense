using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EliteAbilityContainer : MonoBehaviour
{
    [SerializeField] private EnemyRenderViewer _renderView;
    [SerializeField] private ParticleView _bloodyAura;

    private readonly List<EliteEnemy> _eliteEnemies = new List<EliteEnemy>(4);

    private int _randomIndex = 0;
    private IEnemySoundContainer _soundContainer;

    private void OnValidate()
    {
        _renderView ??= GetComponent<EnemyRenderViewer>();
    }

    private void Awake()
    {
        _eliteEnemies.Add(new BloodyLord(Color.red, _renderView, transform, _soundContainer, _bloodyAura));
        _randomIndex = Random.Range(0, _eliteEnemies.Count);
    }

    private void OnEnable()
    {
        _eliteEnemies[_randomIndex].ActivateAbility();
        _eliteEnemies[_randomIndex].SetColor();
    }

    private void OnDisable()
    {
        _eliteEnemies[_randomIndex].DeactivateAbility();
    }

    [Inject]
    private void Constructor(IEnemySoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }
}