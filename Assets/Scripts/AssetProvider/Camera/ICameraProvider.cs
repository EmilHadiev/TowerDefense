using UnityEngine;

public interface ICameraProvider
{
    void Punch();
    void Vibrate();

    public void SetPosition(Vector3 position, Quaternion quaternion);
}