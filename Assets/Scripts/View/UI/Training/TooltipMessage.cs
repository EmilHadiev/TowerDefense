using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipMessage : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private HintArrow _arrow;
    [SerializeField] private RectTransform _container;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Vector2 _offset;

    public event Action Closed;

    private void OnValidate()
    {
        _container ??= GetComponent<RectTransform>();
    }

    private void OnEnable() => _exitButton.onClick.AddListener(Close);

    private void OnDisable() => _exitButton.onClick.RemoveListener(Close);

    public void ShowMessage(string message, HintArrow arrow)
    {
        _text.text = message;
        _arrow = arrow;

        UpdatePosition();
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector2 arrowScreenPos = RectTransformUtility.WorldToScreenPoint(
        null,
        _arrow.transform.position
    );

        // Если стрелка в правой части экрана, смещаем подсказку влево, и наоборот
        Vector2 adjustedOffset = _offset;
        if (arrowScreenPos.x > Screen.width / 2)
        {
            adjustedOffset.x = -Mathf.Abs(_offset.x); // Смещаем влево
        }
        else
        {
            adjustedOffset.x = Mathf.Abs(_offset.x); // Смещаем вправо
        }

        _container.position = _arrow.transform.position + (Vector3)adjustedOffset;

        // Остальной код (pivot и проверка границ) как в предыдущем примере
        float pivotX = (arrowScreenPos.x > Screen.width / 2) ? 1 : 0;
        float pivotY = (arrowScreenPos.y > Screen.height / 2) ? 1 : 0;
        _container.pivot = new Vector2(pivotX, pivotY);

        // Дополнительно можно применить Clamp, как в первом варианте
    }

    public void EnableToggle(bool isOn) => gameObject.SetActive(isOn);

    private void Close() => Closed?.Invoke();
}
