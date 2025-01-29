using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const int AttackButton = 0;
    private const float SpeedRotation = 15f;
    private const float SmoothTime = 0.1f;
    private readonly IPlayer _player;

    public event Action Attacked;

    private Vector3 _smoothDirection;
    private Vector3 _smoothVelocity;

    public DesktopInput(IPlayer player)
    {
        _player = player;
    }

    public void Tick()
    {
        Attack();
        Rotate();
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
        _smoothDirection = Vector3.SmoothDamp(_smoothDirection, direction, ref _smoothVelocity, SmoothTime); // Сглаживание
        Quaternion targetRotation = Quaternion.LookRotation(_smoothDirection);
        return targetRotation;
    }

    private Vector3 GetTargetPosition(RaycastHit hit)
    {
        Vector3 targetPosition = hit.point;
        targetPosition.y = _player.Transform.position.y; // Игнорируем высоту
        return targetPosition;
    }
}