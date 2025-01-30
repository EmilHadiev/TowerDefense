using UnityEngine;

public static class PhysicsDebug
{
    public static void DrawDebug(Vector3 position, float radius, float seconds = 1)
    {
        Debug.DrawRay(position, radius * Vector3.up, Color.red, seconds);
        Debug.DrawRay(position, radius * Vector3.down, Color.red, seconds);
        Debug.DrawRay(position, radius * Vector3.left, Color.red, seconds);
        Debug.DrawRay(position, radius * Vector3.right, Color.red, seconds);
        Debug.DrawRay(position, radius * Vector3.forward, Color.red, seconds);
        Debug.DrawRay(position, radius * Vector3.back, Color.red, seconds);
    }
}