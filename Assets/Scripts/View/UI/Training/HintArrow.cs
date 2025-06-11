using UnityEngine;

public class HintArrow : MonoBehaviour
{
    [SerializeField] private RectTransform _arrow;
    [SerializeField] private RectTransform _target;
    [SerializeField] private float _shakeDistance = 10f;
    [SerializeField] private float _shakeSpeed = 10f;
    [SerializeField] private float _distanceFromTarget = 50f; 

    private Vector2 _baseArrowPosition;

    private void OnValidate()
    {
        if (_arrow == null)
            _arrow = GetComponent<RectTransform>();
    }

    private void Awake()
    {
        if (_arrow != null)
        {
            _baseArrowPosition = _arrow.anchoredPosition;
        }
    }

    void Update()
    {
        if (_target == null || _arrow == null) return;

        UpdateArrowPositionAndRotation();
        CalculateOffset();
    }

    public void SetTarget(RectTransform newTarget)
    {
        ClearTarget();
        _target = newTarget;
        EnableToggle(true);
    }

    public void ClearTarget()
    {
        _target = null;
    }

    public void EnableToggle(bool isOn) => gameObject.SetActive(isOn);

    private void UpdateArrowPositionAndRotation()
    {
        Vector2 direction = GetDirection();

        SetArrowRotation(direction);

        SetArrowPosition(direction);

        _baseArrowPosition = _arrow.anchoredPosition;
    }

    private void SetArrowPosition(Vector2 direction)
    {
        Vector2 targetScreenPosition = _target.position;
        Vector2 arrowPosition = targetScreenPosition - direction * _distanceFromTarget;
        _arrow.position = arrowPosition;
    }

    private void SetArrowRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        _arrow.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void CalculateOffset()
    {
        float offset = Mathf.Sin(Time.time * _shakeSpeed) * _shakeDistance;
        _arrow.anchoredPosition = _baseArrowPosition + GetDirection() * offset;
    }

    private Vector2 GetDirection() => (_target.position - _arrow.position).normalized;
}