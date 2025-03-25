using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulletSwitchView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _bulletImage;
    [SerializeField] private TMP_Text _bulletNameText;
    [SerializeField] private TMP_Text _bulletDescriptionText;
    [SerializeField] private TMP_Text _bulletPriceText;

    private BulletData _data;

    public event Action Clicked;

    public void Initialize(BulletData bulletData)
    {
        _data = bulletData;
        ShowData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        Debug.Log(gameObject.name);
    }

    private void ShowData()
    {
        _bulletImage.sprite = _data.Sprite;
        _bulletDescriptionText.text = _data.Description;
        _bulletNameText.text = _data.Name;
    }
}