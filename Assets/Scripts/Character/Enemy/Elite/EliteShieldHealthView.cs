using TMPro;
using UnityEngine;

public class EliteShieldHealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public void SetHealth(int health)
    {
        _text.text = health.ToString();
    }
}