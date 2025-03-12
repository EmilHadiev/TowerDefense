using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using YG;
using Zenject;

public class CoinStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private ICoinStorage _coinStorage;

    private void OnEnable() => _coinStorage.CoinsChanged += OnCoinsChanged;

    private void OnDisable() => _coinStorage.CoinsChanged -= OnCoinsChanged;

    private void Start() => OnCoinsChanged(_coinStorage.Coins);

    [Inject]
    private void Constructor(ICoinStorage coinStorage) => _coinStorage = coinStorage;

    private void OnCoinsChanged(int coins) => _text.text = coins.ToString();
}