using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private ISceneLoader _loader;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadMainMenu);
    }

    [Inject]
    private void Constructor(ISceneLoader sceneLoader)
    {
        _loader = sceneLoader;
    }

    private void LoadMainMenu()
    {
        _loader.SwitchTo(AssetProvider.SceneMainMenu);
    }
}