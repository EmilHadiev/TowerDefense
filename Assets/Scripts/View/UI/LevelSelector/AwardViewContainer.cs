using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AwardViewContainer : MonoBehaviour
{
    [SerializeField] private Image _awardImage;
    [SerializeField] private TMP_Text _awardNameText;

    private AwardGiver _awardGiver;

    [Inject]
    private void Constructor(AwardGiver awardGiver)
    {
        _awardGiver = awardGiver;
    }

    public bool TryShow()
    {
        if (_awardGiver.IsRewardLevel() == false)
            return false;            

        gameObject.SetActive(true);       

        _awardImage.sprite = _awardGiver.GetRewardSprite();
        _awardNameText.text = _awardGiver.GetRewardDescription();

        return true;
    }
}