using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeValueText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;

    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;
    private Upgrader _upgrader;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(ICoinStorage coinStorage, ISoundContainer soundContainer, Upgrader upgrader)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _upgrader = upgrader;
        Show(upgrader.Data);
    }

    private void Show(UpgradeData data)
    {
        _upgradeImage.sprite = data.Sprite;
        _upgradeNameText.text = data.Name;
        _upgradeValueText.text = data.Value.ToString();
        _costText.text = data.Cost.ToString();
    }

    private void OnClicked()
    {
        if (_coinStorage.TrySpend(_upgrader.Data.Cost))
        {
            _upgrader.Upgrade();
            _soundContainer.Play(SoundType.SpendCoin);
        }
    }
}