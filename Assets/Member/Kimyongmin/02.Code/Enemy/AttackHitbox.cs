using System;
using System.Collections;
using UnityEngine;

public enum BoxType
{
    Box,
    Sector
}
public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private BoxType boxType;
    private LineRenderer _lineRenderer;

    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float LinePos {get; private set;}
    [SerializeField] private float duration;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public IEnumerator ShowHitbox(Vector2 targetPos)
    {
        _lineRenderer.SetPosition(1, targetPos);
        _lineRenderer.enabled = true;
        
        float timer = 0;
        
        float t = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            
            t =  timer / duration;
             
            float size = Mathf.Lerp(0, Range, Mathf.Clamp01(t));
            _lineRenderer.SetPosition(1, new Vector3(size,targetPos.y,0));
            yield return null;
        }
        
        _lineRenderer.enabled = false;
    }
}
