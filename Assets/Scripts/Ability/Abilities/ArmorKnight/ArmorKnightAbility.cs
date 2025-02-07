using UnityEngine;

public class ArmorKnightAbility : MonoBehaviour
{
    private IAbility _ability;

    private void Awake() => _ability = new EnemyMirror(transform);

    private void OnEnable() => _ability.Activate();
}