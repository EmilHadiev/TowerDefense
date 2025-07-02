using UnityEngine;

public class PoisonLord : EliteEnemy
{
    private readonly PoisonAura _poisonAura;
    private readonly IHealth _health;

    public PoisonLord(Color viewColor, EnemyRenderViewer renderViewer, PoisonAura poisonAura, IHealth enemyHealth) : base(viewColor, renderViewer)
    {
        _poisonAura = poisonAura;
        _poisonAura.Deactivate();
        _health = enemyHealth;
    }

    public override void ActivateAbility()
    {
        _health.Died += OnEnemyDied;
        _poisonAura.Activate();
    }

    public override void DeactivateAbility()
    {
        _health.Died -= OnEnemyDied;
        _poisonAura.Deactivate();
    }

    private void OnEnemyDied()
    {
        _poisonAura.Separate();
    }
}