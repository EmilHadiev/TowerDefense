using UnityEngine;

public class BulletSwitchContainer : MonoBehaviour
{
    [SerializeField] private BulletSwitchView _bulletViewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private BulletData[] _data;

    private void Awake() => CreateTemplates();

    private void CreateTemplates()
    {
        for (int i = 0; i < _data.Length; i++)
            CreateTemplate(_data[i]);
    }

    private void CreateTemplate(BulletData data)
    {
        BulletSwitchView view = Instantiate(_bulletViewTemplate, _container);
        view.Initialize(data);
    }
}