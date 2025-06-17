using TMPro;
using UnityEngine;
using Zenject;

public class ProfitsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _profitsVew;

    private IProfitContainer _profitContainer;

    private void OnEnable()
    {
        _profitsVew.text = _profitContainer.Profits.ToString();
    }

    [Inject]
    private void Constructor(IProfitContainer profitContainer)
    {
        _profitContainer = profitContainer;
    }
}