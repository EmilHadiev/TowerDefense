using UnityEngine;

[RequireComponent(typeof(UpgradeViewRender))]
[RequireComponent(typeof(UpgradeHandler))]
public class UpgradeView : MonoBehaviour
{
    [SerializeField] private UpgradeViewRender _render;
    [SerializeField] private UpgradeHandler _handler;

    private void OnValidate()
    {
        _render ??= GetComponent<UpgradeViewRender>();
        _handler ??= GetComponent<UpgradeHandler>();
    }

    private void OnEnable()
    {
        _handler.Upgraded += _render.UpdateDescription;
        _handler.AttackSpeedFilled += _render.ShowFilledAttackSpeed;
        _handler.DamageFilled += _render.ShowFilledDamage;
    }

    private void OnDisable()
    {
        _handler.Upgraded -= _render.UpdateDescription;
        _handler.AttackSpeedFilled -= _render.ShowFilledAttackSpeed;
        _handler.DamageFilled -= _render.ShowFilledDamage;
    }

    public void Initialize(ICoinStorage coinStorage, GunData gunData, IPlayerSoundContainer playerSoundContainer, IGunPlace gunPlace)
    {
        _render.Initialize(gunData);
        _handler.Initialize(gunData, coinStorage, playerSoundContainer, gunPlace);
    }
}