using System;
using UnityEngine;
using Zenject;

class MobileInput : IInput, ITickable
{
    private const int FirstTouch = 0;

    private readonly PlayerRotator _rotator;
    
    public event Action Attacked;

    public MobileInput(IPlayer player)
    {
        _rotator = new PlayerRotator(player);
    }

    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(FirstTouch);

            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    _rotator.Rotate(touch.position);
                    StartAttack();
                    break;
            }
        }
    }

    private void StartAttack() => Attacked?.Invoke();
}
