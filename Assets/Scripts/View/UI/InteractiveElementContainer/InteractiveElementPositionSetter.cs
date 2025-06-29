using UnityEngine;
using Zenject;

public class InteractiveElementPositionSetter : MonoBehaviour
{
    private const int AttackButton = 0;
    private readonly RaycastHit[] _hits = new RaycastHit[1];

    private Camera _camera;
    private bool _isDesktop;

    private EnvironmentData _envData;
    private IFactoryParticle _particleFactory;
    private ParticleView _particle;

    private bool _isWork = false;

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
        _isDesktop = _envData.IsDesktop;

        _particle = _particleFactory.Create(AssetProvider.ParticleInteractiveElementPlacePath);
        StopParticle();
    }

    private void Update()
    {
        if (_isWork == false)
            return;

        if (_isDesktop)
            GetMousePosition();
    }

    [Inject]
    private void Constructor(IFactoryParticle factory, EnvironmentData environment, IInput input)
    {
        _particleFactory = factory;
        _envData = environment;
    }

    public void EnableToggle(bool isWork)
    {
        _isWork = isWork;

        if (_isWork == false)
            StopParticle();
    }

    private void GetTouchPosition()
    {

    }

    private void GetMousePosition()
    {
        if (Input.GetMouseButton(AttackButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.RaycastNonAlloc(ray, _hits) > 0)
            {
                for (int i = 0; i < _hits.Length; i++)
                {
                    if (_hits[i].transform.TryGetComponent(out Map map))
                    {
                        Position = _hits[i].point;
                        PlayerView();
                    }
                }
            }
        }
    }

     void PlayerView()
    {
        _particle.transform.position = Position;
        _particle.Play();
    }

    private void StopParticle()
    {
        if (_particle != null)
            _particle.Stop();
    }
}