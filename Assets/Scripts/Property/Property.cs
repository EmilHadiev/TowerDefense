using System;

public class Property
{
    public event Action<float> Changed;

    private float _value;

    public Property(float value)
    {
        _value = value;
    }

    public float Value
    {
        get => _value;
        set
        {
            if (IsValid(value) == false)
                throw new ArgumentException(nameof(value));

            float oldValue = _value;
            _value = value;

            if (_value.CompareTo(oldValue) != 0)
                Changed?.Invoke(_value);
        }
    }

    private bool IsValid(float value)
    {
        if (value < 0)
            return false;

        return true;
    }
}