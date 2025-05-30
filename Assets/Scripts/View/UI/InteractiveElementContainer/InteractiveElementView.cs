using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class InteractiveElementView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _elementImage;
    [SerializeField] private Image _selectBackgroundImage;
    [SerializeField] private TMP_Text _elementNameText;
    [SerializeField] private TMP_Text _elementPriceText;

    private InteractiveElementData _data;

    public event Action<InteractiveElementData> Selected;

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected?.Invoke(_data);
        _selectBackgroundImage.enabled = true;
    }

    public void Show(InteractiveElementData data)
    {
        _data = data;
        LocalizedText text = _data.GetLocalizedText(YG2.lang);

        _elementImage.sprite = _data.Sprite;
        _elementNameText.text = text.Name;
        _elementPriceText.text = _data.Price.ToString();
    }

    public void HideSelectBackground() => _selectBackgroundImage.enabled = false;
}
