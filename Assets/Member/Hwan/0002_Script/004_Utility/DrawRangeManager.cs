using UnityEngine;

public class DrawRangeManager : MonoSingleton<DrawRangeManager>
{
    public LineRenderer lineRenderer;
    public bool closed = true;

    protected override void Awake()
    {
        base.Awake();

        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawBox(Vector2 size, float angle, Vector3 position, Vector3 offset)
    {
        lineRenderer.loop = true;
        lineRenderer.positionCount = 4;

        float halfX = size.x / 2;
        float halfY = size.y / 2;

        Vector3[] points = { new Vector2(-halfX, -halfY), new Vector2(-halfX, halfY),
        new Vector2(halfX, halfY), new Vector2(halfX, -halfY)};

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Quaternion.Euler(0, 0, angle) * (points[i] + offset) + position;
        }

        lineRenderer.SetPositions(points);
    }

}
