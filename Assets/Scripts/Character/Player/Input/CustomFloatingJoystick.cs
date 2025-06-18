using UnityEngine.EventSystems;

public class CustomFloatingJoystick : Joystick
{
    private void OnDisable() => StopCalculate();

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    private void StopCalculate()
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(null);
    }
}