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
        _handler.Upgraded += Show;
    }

    private void OnDisable()
    {
        _handler.Upgraded -= Show;
    }

    public void Initialize(ICoinStorage coinStorage, GunData gunData, IPlayerSoundContainer playerSoundContainer)
    {
        _render.Initialize(gunData);
        _handler.Initialize(gunData, coinStorage, playerSoundContainer);
        Show();
    }

    private void Show()
    {
        _render.UpdateDescription();
    }
}