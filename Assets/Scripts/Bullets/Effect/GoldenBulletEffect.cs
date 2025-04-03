using UnityEngine;

class GoldenBulletEffect : IBulletEffectHandler
{
    private const int Coins = 1;

    private readonly ICoinStorage _coinStorage;

    public GoldenBulletEffect(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    public void HandleEffect(Collider enemy) => AddCoins();

    private void AddCoins() => _coinStorage.Add(Coins);
}
