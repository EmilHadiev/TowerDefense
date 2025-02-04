using TMPro;
using UnityEngine;
using Zenject;

public class CoinStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private ICoinStorage _coinStorage;

    private void OnEnable() => _coinStorage.CoinsChanged += OnCoinsChanged;

    private void OnDisable() => _coinStorage.CoinsChanged -= OnCoinsChanged;

    [Inject]
    private void Constructor(ICoinStorage coinStorage) => _coinStorage = coinStorage;

    private void OnCoinsChanged(int coins) => _text.text = coins.ToString();
}
