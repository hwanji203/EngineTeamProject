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

    public void DrawBox(Vector2 size, float angleDeg, Vector3 position)
    {
        lineRenderer.loop = true;
        lineRenderer.positionCount = 4;

        // 로컬 정점(원점 기준)
        var hx = size.x * 0.5f;
        var hy = size.y * 0.5f;
        Vector3[] v = new Vector3[]
        {
            new(-hx, -hy, 0), new(-hx, hy, 0),
            new(hx,  hy, 0),  new(hx, -hy, 0)
        };

        Quaternion rot = Quaternion.Euler(0, 0, angleDeg);
        for (int i = 0; i < v.Length; i++)
            v[i] = rot * v[i] + position;

        lineRenderer.SetPositions(v);
    }

}
