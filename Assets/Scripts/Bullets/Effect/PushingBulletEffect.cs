using DG.Tweening;
using UnityEngine;

public class PushingBulletEffect : IBulletEffectHandler
{
    private const float Duration = 0.25f;
    private const int Force = 5;

    private Tween _currentTween;

    public void HandleEffect(Collider enemy)
    {
        if (_currentTween != null)
            _currentTween.Kill();

        _currentTween = enemy.transform.DOLocalMoveZ(GetPushPosition(enemy.transform), Duration).OnUpdate(() => Updated(enemy.gameObject));
    }

    private void Updated(GameObject gameObject)
    {
        if (gameObject.activeInHierarchy == false)
            _currentTween.Kill();
    }

    private float GetPushPosition(Transform enemy) => enemy.localPosition.z + Force;
}