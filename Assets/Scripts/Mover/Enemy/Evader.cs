using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Evader : MonoBehaviour, IEvadable
{
    [SerializeField] private float _dodgeDistance = 3f;
    [SerializeField] private float _dodgeDuration = 0.5f;
    [SerializeField] private int _dodgeInterval = 1;

    private bool _isDodging;
    private Tween _activeTween;
    private IPlayer _player;

    private float _timeAfterDodge;

    private void Awake()
    {
        //_movable = GetComponent<IMovable>();
    }

    private void OnEnable()
    {
        ResetInterval();
    }

    private void OnDisable()
    {
        _activeTween?.Kill();
    }

    private void Update()
    {
        if (_timeAfterDodge >= _dodgeDistance)
            ResetInterval();

        _timeAfterDodge += Time.deltaTime;
    }

    [Inject]
    private void Construct(IPlayer player)
    {
        _player = player;
    }

    private void OnAttacked() => Dodge();

    public void Dodge()
    {
        if (CanDodge() == false)
            return;

        _isDodging = true;
        StopDodge();

        if (TryGetDodgePosition(out Vector3 dodgePos))
        {
            ExecuteDodge(dodgePos);
        }
        else
        {
            // Пробуем противоположное направление
            if (TryCalculateOppositeDodgePosition(out dodgePos))
            {
                ExecuteDodge(dodgePos);
            }
            else
            {
                ResetDodgeState();
            }
        }
    }

    private void StopDodge()
    {
        //_movable.StopMove(); // Отключаем движение через интерфейс
        _activeTween?.Kill();
    }

    private bool CanDodge() => _isDodging == false && _timeAfterDodge >= _dodgeInterval;

    private bool TryGetDodgePosition(out Vector3 result)
    {
        Vector3 toPlayer = (_player.Transform.position - transform.position).normalized;
        bool dodgeRight = Random.value > 0.5f;
        Vector3 dodgeDir = GetDirection(toPlayer, dodgeRight);

        return TryGetPositionInNavMesh(dodgeDir, out result);
    }

    private bool TryCalculateOppositeDodgePosition(out Vector3 result)
    {
        Vector3 toPlayer = (_player.Transform.position - transform.position).normalized;
        Vector3 oppositeDir = GetDirection(toPlayer, Random.value <= 0.5f);

        return TryGetPositionInNavMesh(oppositeDir, out result);
    }

    private bool TryGetPositionInNavMesh(Vector3 direction, out Vector3 result)
    {
        Vector3 targetPos = transform.position + direction * _dodgeDistance;
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, _dodgeDistance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private Vector3 GetDirection(Vector3 direction, bool right)
    {
        return right ? Vector3.Cross(direction, Vector3.up).normalized :
                      Vector3.Cross(Vector3.up, direction).normalized;
    }

    private void ExecuteDodge(Vector3 targetPos)
    {
        _activeTween = transform.DOMove(targetPos, _dodgeDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(CompleteDodge)
            .OnKill(ResetDodgeState);
    }

    private void CompleteDodge()
    {
        //_movable.StartMove(); // Включаем движение через интерфейс
        _isDodging = false;
    }

    private void ResetDodgeState()
    {
        //_movable?.StartMove(); // Всегда восстанавливаем движение
        _isDodging = false;
    }

    private void ResetInterval()
    {
        _timeAfterDodge = 0;
    }
}