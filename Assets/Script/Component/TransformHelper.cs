using UnityEngine;

public static class TransformHelper
{
    public static void UpdateRotation(Transform transform, Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}