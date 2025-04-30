using UnityEngine;

public class EnemyMirror : IAbility
{
    private Transform _enemy;

    public EnemyMirror(Transform enemy)
    {
        _enemy = enemy;
    }

    public void Activate() => RandomActivate();

    private Vector3 GetMirrorScale(Transform transform) => new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    private void RandomActivate()
    {
        int randomValue = Random.Range(0, 2);

        if (randomValue % 2 == 0)
            _enemy.localScale = GetMirrorScale(_enemy);
    }
}