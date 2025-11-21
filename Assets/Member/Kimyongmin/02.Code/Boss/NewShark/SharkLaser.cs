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
            if (dir.x < 0)
                StartCoroutine(FlipShark());
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
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
            if (dir.x < 0)
                StartCoroutine(FlipLaser());
        }

        public void ShootLaser()
        {
            laser.SetActive(true);
            
        }

        private float _flipDelay = 1.5f;
        private IEnumerator FlipLaser()
        {
            laser.transform.localRotation = Quaternion.Euler(new Vector3(0,180,180));
            yield return new WaitForSeconds(_flipDelay);
            laser.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        
        private IEnumerator FlipShark()
        {
            transform.parent.DOScale(new Vector3(1, -1, 1), 0);
            yield return new WaitForSeconds(_flipDelay);
            transform.parent.DOScale(new Vector3(1, 1, 1), 0);
            transform.parent.rotation = Quaternion.Euler(new Vector3(0,transform.parent.rotation.y,0));
        }
    }
}
