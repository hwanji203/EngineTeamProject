using System;
using UnityEngine;

public class LineCreater : MonoBehaviour
{
    [SerializeField] private GameObject pointpool;
    [SerializeField]private Transform[] points;
    private LineRenderer line;

    private void Start()
    {
        points = new Transform[pointpool.transform.childCount];
        for (int i = 0; i < pointpool.transform.childCount; i++)
        { 
            points[i] = pointpool.transform.GetChild(i).GetComponent<Transform>();
        }
        line = GetComponent<LineRenderer>();
        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i].position);
        }

    }
}
