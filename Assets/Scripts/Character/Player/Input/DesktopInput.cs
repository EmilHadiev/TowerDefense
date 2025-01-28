using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const int AttackButton = 0;
    private const float SpeeRotation = 25f;
    private readonly IPlayer _player;

    public event Action Attacked;

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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = GetTargetPosition(hit);
            Quaternion targetRotation = GetTargetDirection(targetPosition);

            _player.Transform.rotation = Quaternion.Slerp(_player.Transform.rotation, targetRotation, Time.deltaTime * SpeeRotation);
        }
    }

    private Quaternion GetTargetDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _player.Transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        return targetRotation;
    }

    private Vector3 GetTargetPosition(RaycastHit hit)
    {
        Vector3 targetPosition = hit.point;
        targetPosition.y = _player.Transform.position.y;
        return targetPosition;
    }
}