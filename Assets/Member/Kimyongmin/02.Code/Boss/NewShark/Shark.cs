using System;
using Member.Kimyongmin._02.Code.Boss.SO;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class Shark : MonoBehaviour
    {
        [field:SerializeField] public SharkDataSO SharkData { get; private set; }
        [SerializeField] private Transform target;
        public SharkMovement SharkMovement { get; private set; }
        private Vector3 _moveDir;

        private void Awake()
        {
            SharkMovement = GetComponent<SharkMovement>();
        }

        private void FixedUpdate()
        {
            SharkMovement.RbMove(_moveDir, SharkData.speed);
        }

        public void SetMoveDir(Vector3 dir)
        {
            _moveDir = dir;
        }
        public Vector3 GetTargetDir()
        {
            return (target.position - transform.position).normalized;
        }
    }
}
