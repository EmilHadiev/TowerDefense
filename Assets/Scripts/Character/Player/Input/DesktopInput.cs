using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const int AttackButton = 0;
    private readonly Camera _camera;
    private readonly IPlayer _player;

    public event Action Attacked;

    public DesktopInput(IPlayer player)
    {
        _player = player;
        _camera = Camera.main;
    }

    public void Tick()
    {
        Rotate();
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButton(AttackButton))
            Attacked?.Invoke();
    }

    private void Rotate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 mousePosition = GetMousePoint(hit);

            Vector3 direction = GetDirection(mousePosition);

            _player.Transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private Vector3 GetMousePoint(RaycastHit hit)
    {
        Vector3 mousePosition = hit.point;
        mousePosition.y = _player.Transform.position.y;
        return mousePosition;
    }

    private Vector3 GetDirection(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - _player.Transform.position;
        direction.Normalize();
        return direction;
    }
}