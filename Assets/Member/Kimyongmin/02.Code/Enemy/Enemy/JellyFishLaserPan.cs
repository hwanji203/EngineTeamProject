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
            _points.Clear();
            
            Transform currentTransform = transform;
    
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                Vector3 worldPos = _lineRenderer.GetPosition(i);
                Vector2 localPos = currentTransform.InverseTransformPoint(worldPos); 
                _points.Add(localPos);
            }

            _edgeCollider2D.SetPoints(_points);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(_playerTag))
            {
                if (other.TryGetcomponentInParent(out Player player))
                {
                    player.GetDamage(1, Vector2.zero);
                }
            }
        }

        private void OnDestroy()
        {
            _edgeCollider2D.enabled = false;
        }
    }
}