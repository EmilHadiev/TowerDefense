using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ElementSetMode : MonoBehaviour
{
    [SerializeField] private Button _setElementButton;
    [SerializeField] private InteractiveElementPositionSetter _positionSetter;
    [SerializeField] private InteractiveElementViewContainer _container;
    [SerializeField] private GridMeshGenerator _gridGenerator;

    private IInput _input;

    private bool _isWork = false;

    private void OnValidate()
    {
        _positionSetter ??= FindObjectOfType<InteractiveElementPositionSetter>();
        _container ??= FindObjectOfType<InteractiveElementViewContainer>();
        _gridGenerator ??= FindObjectOfType<GridMeshGenerator>();
    }

    private void OnEnable()
    {
        _setElementButton.onClick.AddListener(OpenOrSet);
    }

    private void OnDisable()
    {
        _setElementButton.onClick.RemoveListener(Stop);
    }

    [Inject]
    public void Constructor(IInput input)
    {
        _input = input;
    }

    /// <summary>
    /// if the mod is open, then the next button press will install the element on the map
    /// </summary>
    public void OpenOrSet()
    {
        if (_container.IsAvailableElementPresent == false)
        {
            Stop();
            return;
        }

        if (_isWork)
        {
            SetElement();
            return;
        }

        _positionSetter.EnableToggle(true);
        _gridGenerator.gameObject.SetActive(true);
        _input.Stop();
        _isWork = true;
    }

    private void Stop()
    {
        _isWork = false;
        _positionSetter.EnableToggle(false);
        _gridGenerator.gameObject.SetActive(false);
        _input.Continue();
    }

    private void SetElement()
    {
        _container.SetElement();
        Stop();
    }
}