using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable, IInitializable, IDisposable, IDesktopInput
{
    private const int AttackButton = 0;
    private const float SpeedRotation = 15f;
    private const float SmoothTime = 0.1f;

    private readonly IPlayer _player;
    private readonly InputSystem _inputSystem;

    private Vector3 _smoothDirection;
    private Vector3 _smoothVelocity;

    public event Action Attacked;
    public event Action<int> SwitchBulletButtonClicked;

    public DesktopInput(IPlayer player)
    {
        _player = player;
        _inputSystem = new InputSystem();
    }

    public void Initialize()
    {
        _inputSystem.Enable();

        _inputSystem.Player.SwitchToBullet0.performed += ctx => SwitchTo(0);
        _inputSystem.Player.SwitchToBullet1.performed += ctx => SwitchTo(1);
        _inputSystem.Player.SwitchToBullet2.performed += ctx => SwitchTo(2);
        _inputSystem.Player.SwitchToBullet3.performed += ctx => SwitchTo(3);
        _inputSystem.Player.SwitchToBullet4.performed += ctx => SwitchTo(4);
    }

    public void Tick()
    {
        Attack();
        Rotate();
    }

    public void Dispose()
    {
        _inputSystem.Player.SwitchToBullet0.performed -= ctx => SwitchTo(0);
        _inputSystem.Player.SwitchToBullet1.performed -= ctx => SwitchTo(1);
        _inputSystem.Player.SwitchToBullet2.performed -= ctx => SwitchTo(2);
        _inputSystem.Player.SwitchToBullet3.performed -= ctx => SwitchTo(3);
        _inputSystem.Player.SwitchToBullet4.performed -= ctx => SwitchTo(4);

        _inputSystem.Disable();
    }

    private void Attack()
    {
        if (Input.GetMouseButton(AttackButton))
            Attacked?.Invoke();
    }

    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = GetTargetPosition(hit);
            Quaternion targetRotation = GetTargetDirection(targetPosition);

            _player.Transform.rotation = Quaternion.Slerp(_player.Transform.rotation, targetRotation, Time.deltaTime * SpeedRotation);
        }
    }

    private Quaternion GetTargetDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _player.Transform.position).normalized;
        direction.y = 0; // Игнорируем наклон по оси Y
        _smoothDirection = Vector3.SmoothDamp(_smoothDirection, direction, ref _smoothVelocity, SmoothTime);
        Quaternion targetRotation = Quaternion.LookRotation(_smoothDirection);
        return targetRotation;
    }

    private Vector3 GetTargetPosition(RaycastHit hit)
    {
        Vector3 targetPosition = hit.point;
        targetPosition.y = _player.Transform.position.y;
        return targetPosition;
    }

    private void SwitchTo(int index) => SwitchBulletButtonClicked?.Invoke(index);
}