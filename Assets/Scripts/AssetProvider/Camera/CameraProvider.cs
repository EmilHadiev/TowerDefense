using DG.Tweening;
using UnityEngine;

public class CameraProvider : MonoBehaviour, ICameraProvider
{
    [SerializeField] private Vector3 _defaultTopPosition = new Vector3(0, 48, 0);
    [SerializeField] private Quaternion _defaultTopRotation = new Quaternion(90, 0, 0, 0);

    private Camera _camera;
    private readonly Vector3 _punchVector = new Vector3(0.1f, 0.1f, 0);
    private readonly Vector3 _shakeVector = new Vector3(0.1f, 0.1f, 0);

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    private void Awake()
    {
        _camera = Camera.main;

        _defaultPosition = _camera.transform.position;
        _defaultRotation = _camera.transform.rotation;
    }

    public void Punch()
    {
        if (DOTween.IsTweening(_camera.transform))
            return;

        _camera.transform.DOPunchPosition(
            punch: _punchVector,
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
            strength: _shakeVector,
            vibrato: 50,
            randomness: 90,
            snapping: false,
            fadeOut: false)
            .SetUpdate(UpdateType.Normal, true);
    }

    public void SetDefaultPosition()
    {
        SetPosition(_defaultPosition, _defaultRotation);
    }

    public void SetTopView()
    {
        SetPosition(_defaultTopPosition, _defaultTopRotation);
    }

    private void SetPosition(Vector3 position, Quaternion quaternion)
    {
        _camera.transform.position = position;
        _camera.transform.rotation = quaternion;
    }
}
