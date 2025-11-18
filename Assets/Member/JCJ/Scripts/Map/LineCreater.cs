using System;
using UnityEngine;

public class LineCreater : MonoBehaviour
{
    [SerializeField] private GameObject pointPool;
    [SerializeField]private Transform[] points;
    private LineRenderer line;

    private void Start()
    {
        points = new Transform[pointPool.transform.childCount];
        for (int i = 0; i < pointPool.transform.childCount; i++)
        { 
            points[i] = pointPool.transform.GetChild(i).GetComponent<Transform>();
        }
        line = GetComponent<LineRenderer>();
        line.positionCount = points.Length;
    }

    private void FixedUpdate()
    {
        if (points != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                line.SetPosition(i, points[i].position);
            }
        }
    }
}
