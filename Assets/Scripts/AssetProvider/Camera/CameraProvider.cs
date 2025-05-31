using DG.Tweening;
using UnityEngine;

public class CameraProvider : ICameraProvider
{
    private readonly Camera _camera;

    public CameraProvider()
    {
        _camera = Camera.main;
    }

    public void Punch()
    {
        if (DOTween.IsTweening(_camera.transform))
            return;

        _camera.transform.DOPunchPosition(
          punch: new Vector3(0.1f, 0.1f, 0),
          duration: 0.5f,
          vibrato: 10,
          elasticity: 0.25f);
    }

    public void Vibrate()
    {
        if (DOTween.IsTweening(_camera.transform))
            return;

        _camera.transform.DOShakePosition(
            duration: 1f,
            strength: new Vector3(0.1f, 0.1f, 0),
            vibrato: 50,
            randomness: 90,
            snapping: false,
            fadeOut: false);
    }
}
