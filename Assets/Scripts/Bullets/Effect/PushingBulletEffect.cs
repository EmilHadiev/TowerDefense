using DG.Tweening;
using UnityEngine;

public class PushingBulletEffect : IBulletEffectHandler
{
    private const float Duration = 0.25f;
    private const float Force = 5f;

    private readonly Transform _bullet;

    private Tween _currentTween;

    public PushingBulletEffect(Transform bullet)
    {
        _bullet = bullet;
    }

    public void HandleEffect(Collider enemy)
    {
        if (_currentTween != null && _currentTween.IsActive())
            _currentTween.Kill();

        Vector3 pushPosition = GetPushPosition(enemy);

        _currentTween = enemy.transform.DOMove(pushPosition, Duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => Updated(enemy.gameObject));
    }

    private Vector3 GetPushPosition(Collider enemy)
    {
        return enemy.transform.position + _bullet.transform.forward.normalized * Force;
    }

    private void Updated(GameObject gameObject)
    {
        if (!gameObject.activeInHierarchy && _currentTween != null)
            _currentTween.Kill();
    }
}