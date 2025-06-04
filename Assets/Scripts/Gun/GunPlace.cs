using UnityEngine;
using Zenject;

public class GunPlace : MonoBehaviour, IGunPlace
{
    private Gun _currentGun;
    private IPlayerSoundContainer _sound;

    public IGun CurrentGun => _currentGun;

    [Inject]
    private void Constructor(IPlayerSoundContainer playerSound)
    {
        _sound = playerSound;
    }

    private void OnEnable()
    {
        Gun gun = GetComponentInChildren<Gun>();
        SetGun(gun);
    }

    public void SetGun(Gun gun)
    {
        _currentGun?.gameObject.SetActive(false);
        _currentGun = gun;
        _currentGun.transform.parent = transform;
        _currentGun.gameObject.SetActive(true);
        _sound.Play(SoundName.SwitchBullet);
    }
}