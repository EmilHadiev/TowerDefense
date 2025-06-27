using DG.Tweening;
using UnityEngine;

public class CameraProvider : MonoBehaviour, ICameraProvider
{
    private Camera _camera;
    private readonly Vector3 PunchVector = new Vector3(0.1f, 0.1f, 0);
    private readonly Vector3 ShakeVector = new Vector3(0.1f, 0.1f, 0);

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Punch()
    {
        if (DOTween.IsTweening(_camera.transform))
            return;

        _camera.transform.DOPunchPosition(
            punch: PunchVector,
            duration: 0.5f,
            vibrato: 10,
            elasticity: 0.25f)
            .SetUpdate(UpdateType.Normal, true);
    }

    public void Vibrate()
    {
        if (DOTween.IsTweening(_camera.transform))
            return;

        _camera.transform.DOShakePosition(
            duration: 1f,
            strength: ShakeVector,
            vibrato: 50,
            randomness: 90,
            snapping: false,
            fadeOut: false)
            .SetUpdate(UpdateType.Normal, true);
    }

    public void SetPosition(Vector3 position, Quaternion quaternion)
    {
        _camera.transform.position = position;
        _camera.transform.rotation = quaternion;
    }
}
