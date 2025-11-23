using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(EdgeCollider2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class JellyFishLaserPan : MonoBehaviour
    {
        private readonly string _playerTag = "Player";

        private EdgeCollider2D _edgeCollider2D;
        private LineRenderer _lineRenderer;
        private List<Vector2> _points = new List<Vector2>();
        private List<Vector2> _previousPoints = new List<Vector2>();

        private void Awake()
        {
            _edgeCollider2D = GetComponent<EdgeCollider2D>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void LateUpdate()
        {
            SyncCollider();
        }

        private void SyncCollider()
        {
            if (_lineRenderer.positionCount == 0)
            {
                _edgeCollider2D.enabled = false;
                _previousPoints.Clear();
                return;
            }
            
            List<Vector2> currentPoints = new List<Vector2>();
            Transform currentTransform = transform;
    
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                Vector3 worldPos = _lineRenderer.GetPosition(i);
                Vector2 localPos = currentTransform.InverseTransformPoint(worldPos);
                currentPoints.Add(localPos);

                if (PointsEqual(currentPoints, _previousPoints))
                {
                    _edgeCollider2D.enabled = false;
                    return;
                }
            }

            _previousPoints = new List<Vector2>(currentPoints);
            
            _points = currentPoints;
            _edgeCollider2D.SetPoints(_points);
            
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(_playerTag))
            {
                if (other.TryGetcomponentInParent(out Player player))
                {
                    player.GetDamage(0.01f, other.gameObject.transform.position * -1);
                }
            }
        }

        private void OnDestroy()
        {
            _edgeCollider2D.enabled = false;
        }
        
        private bool PointsEqual(List<Vector2> current, List<Vector2> previous)
        {
            if (current.Count != previous.Count)
            {
                return false;
            }

            for (int i = 0; i < current.Count; i++)
            {
                if (Vector2.SqrMagnitude(current[i] - previous[i]) > 0.000001f)
                {
                    return false;
                }
            }

            return true;
        }
    }
}