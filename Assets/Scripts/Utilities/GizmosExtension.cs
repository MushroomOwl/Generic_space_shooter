using UnityEngine;

public static class GizmosExtensions
{
    public static void DrawCircle(Vector3 center, float radius, int segmentCount, Color color)
    {
        Gizmos.color = color;

        Vector3 previousPoint = center + new Vector3(1, 0, 0) * radius;
        for (int i = 1; i <= segmentCount; i++)
        {
            float angle = (i * 360) / segmentCount;
            float radian = angle * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0);

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }
}