using UnityEngine;

public class DarkLord : EliteEnemy
{
    private readonly BlackHoleAura _blackHole;
    private readonly IHealth _health;

    public DarkLord(Color viewColor, EnemyRenderViewer renderViewer, BlackHoleAura blackHole, IHealth health) : base(viewColor, renderViewer)
    {
        _blackHole = blackHole;
        _health = health;
        DeactivateBlackHole();
    }

    public override void ActivateAbility()
    {
        _health.Died += OnEnemyDied;
        _blackHole.Activate();
        _blackHole.gameObject.SetActive(true);
    }

    public override void DeactivateAbility()
    {
        _health.Died -= OnEnemyDied;
        DeactivateBlackHole();
    }

    private void OnEnemyDied()
    {
        DeactivateBlackHole();
    }

    private void DeactivateBlackHole()
    {
        _blackHole.Deactivate();
        _blackHole.gameObject.SetActive(false);
    }
}