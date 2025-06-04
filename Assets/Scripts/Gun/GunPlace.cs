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

    public void SetGun(Gun gun)
    {
        _currentGun?.gameObject.SetActive(false);
        _currentGun = gun;
        SetPosition(_currentGun);
        _currentGun.gameObject.SetActive(true);
        _sound.Play(SoundName.SwitchBullet);
    }

    private void SetPosition(Gun gun)
    {
        Vector3 originalLocalPosition = gun.transform.localPosition;
        Quaternion originalLocalRotation = gun.transform.localRotation;
        Vector3 originalLocalScale = gun.transform.localScale;

        gun.transform.SetParent(transform);

        gun.transform.localPosition = originalLocalPosition;
        gun.transform.localRotation = originalLocalRotation;
        gun.transform.localScale = originalLocalScale;
    }
}