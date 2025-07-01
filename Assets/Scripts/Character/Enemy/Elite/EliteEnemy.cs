using UnityEngine;

public abstract class EliteEnemy
{
    private readonly EnemyRenderViewer _renderViewer;
    protected readonly Transform Transform;
    protected readonly Color ViewColor;
    protected readonly IEnemySoundContainer SoundContainer;

    protected EliteEnemy(Color viewColor, EnemyRenderViewer renderViewer, Transform transform, IEnemySoundContainer enemySoundContainer)
    { 
        _renderViewer = renderViewer;
        ViewColor = viewColor;
        Transform = transform;
        SoundContainer = enemySoundContainer;
    }

    public abstract void ActivateAbility();
    public abstract void DeactivateAbility();

    public virtual void SetColor()
    {
        _renderViewer.SetColor(ViewColor);
    }
}