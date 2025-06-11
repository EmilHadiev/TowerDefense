using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveElementSetterView : MonoBehaviour
{
    [SerializeField] private Image _image;

    private readonly Queue<Sprite> _sprites = new Queue<Sprite>(10);

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
    }

    private void Awake()
    {
        _defaultSprite = GetComponent<Image>().sprite;
    }

    private Sprite _defaultSprite;

    public void AddSprite(Sprite sprite)
    {
        _sprites.Enqueue(sprite);

        if (_sprites.Count == 1)
            _image.sprite = sprite;
    }

    public void ShowNextSprite()
    {
        if (_sprites.Count == 0)
            return;

        _sprites.Dequeue();

        if (_sprites.Count > 0)
            _image.sprite = _sprites.Peek();
        else
            SetDefaultSprite();
    }

    private void SetDefaultSprite()
    {
        _image.sprite = _defaultSprite;
    }
}
