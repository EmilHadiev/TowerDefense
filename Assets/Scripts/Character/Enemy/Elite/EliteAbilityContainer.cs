using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EliteAbilityContainer : MonoBehaviour
{
    [SerializeField] private EnemyRenderViewer _renderView;
    [SerializeField] private ParticleView _bloodyAura;
    [SerializeField] private EliteShield _eliteShield;

    private readonly List<EliteEnemy> _eliteEnemies = new List<EliteEnemy>(4);

    private int _randomIndex = 0;
    private IEnemySoundContainer _soundContainer;

    private void OnValidate()
    {
        _renderView ??= GetComponent<EnemyRenderViewer>();
    }

    private void Awake()
    {
        _eliteEnemies.Add(new BloodyLord(Color.red, _renderView, _bloodyAura, transform, _soundContainer));
        _eliteEnemies.Add(new ArmorLord(Color.cyan, _renderView, _eliteShield));
    }

    private void OnEnable()
    {
        _randomIndex = Random.Range(0, _eliteEnemies.Count);
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