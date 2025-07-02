using UnityEngine;

public class ArmorLord : EliteEnemy
{
    private readonly EliteShield _shield;

    public ArmorLord(Color viewColor, EnemyRenderViewer renderViewer, EliteShield shield) 
        : base(viewColor, renderViewer)
    {
        _shield = shield;
        _shield.gameObject.SetActive(false);
    }

    public override void ActivateAbility()
    {
        _shield.gameObject.SetActive(true);
    }

    public override void DeactivateAbility()
    {
        _shield.gameObject.SetActive(false);
    }
}