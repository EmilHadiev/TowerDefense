using UnityEngine;

public static class PhysicsDebug
{
    private static readonly Color DefaultColor = Color.red;

    /// <summary>
    /// default color is red color
    /// </summary>
    public static void DrawDebug(Vector3 position, float radius, float seconds = 1, Color color = default)
    {
        if (color == default)
            color = DefaultColor;

        Debug.DrawRay(position, radius * Vector3.up, color, seconds);
        Debug.DrawRay(position, radius * Vector3.down, color, seconds);
        Debug.DrawRay(position, radius * Vector3.left, color, seconds);
        Debug.DrawRay(position, radius * Vector3.right, color, seconds);
        Debug.DrawRay(position, radius * Vector3.forward, color, seconds);
        Debug.DrawRay(position, radius * Vector3.back, color, seconds);
    }
}