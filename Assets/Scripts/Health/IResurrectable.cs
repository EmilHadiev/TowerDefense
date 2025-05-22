using System;

public interface IResurrectable
{
    public event Action Resurrected;
    void Resurrect();
}