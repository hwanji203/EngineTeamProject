using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkLaser : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        [SerializeField] private GameObject laser;

        [SerializeField] private float maxWidth;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public IEnumerator FocusOn(Vector3 dir)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (dir.x < 0)
                StartCoroutine(FilpSans());
            
            transform.parent.DORotate(new  Vector3(0f, 0f, angle), 0.2f);
        
            _lineRenderer.enabled = true;

            for (int i = 0; i < 50; i++)
            {
                _lineRenderer.endWidth += 0.002f;
                _lineRenderer.startWidth += 0.002f;
                yield return new WaitForSeconds(0.015f);
                
            }

            _lineRenderer.enabled = false;
            _lineRenderer.endWidth = 0;
            _lineRenderer.startWidth = 0;
            ShootLaser();
        }

        public void ShootLaser()
        {
            laser.SetActive(true);
            
        }

        private IEnumerator FilpSans()
        {
            transform.parent.DOScale(new Vector3(1, -1, 1), 0);
            laser.transform.rotation = Quaternion.Euler(0, 180, 180);
            yield return new WaitForSeconds(1.5f);
            transform.parent.DOScale(new Vector3(1, 1, 1), 0);
            laser.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
