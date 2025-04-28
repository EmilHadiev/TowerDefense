using TMPro;
using UnityEngine;

public class PurchaseBulletContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _costText;

    public void SetText(int price) => _costText.text = price.ToString(); 
}