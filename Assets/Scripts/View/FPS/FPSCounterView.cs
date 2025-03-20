using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class FPSCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private WaitForSeconds _delay;
    private FPSCounter _count;

    private void Start()
    {
        _delay = new WaitForSeconds(1);
        StartCoroutine(ShowFPS());
    }

    [Inject]
    private void Constructor(FPSCounter fPSCounter)
    {
        _count = fPSCounter;
    }

    private IEnumerator ShowFPS()
    {
        while (true)
        {
            yield return _delay;
            _text.text = _count.CurrentFPS.ToString();
        }
    }
}
