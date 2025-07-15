using UnityEngine;
using Zenject;

public class DefaultEntryPoint : MonoBehaviour
{
    private LoadingScreen _loadingScreen;

    [Inject]
    private void Constructor(LoadingScreen loadingScreen)
    {
        _loadingScreen = loadingScreen;
    }

    private void Start()
    {
        _loadingScreen.Hide();
    }
}