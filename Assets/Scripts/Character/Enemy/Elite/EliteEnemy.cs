using UnityEngine;

public abstract class EliteEnemy
{
    private readonly EnemyRenderViewer _renderViewer;
    protected readonly Color ViewColor;

    protected EliteEnemy(Color viewColor, EnemyRenderViewer renderViewer)
    { 
        _renderViewer = renderViewer;
        ViewColor = viewColor;
    }

    public abstract void ActivateAbility();
    public abstract void DeactivateAbility();

    public virtual void SetColor()
    {
        _renderViewer.SetColor(ViewColor);
    }
}