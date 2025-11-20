using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkLaserD : MonoBehaviour
    {
        private float _damage;
        private Vector3 _parentPos;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetcomponentInParent(out Player player))
            {
                player.GetDamage(_damage, _parentPos);
            }
        }

        public void LaserSetting(float damage, Vector3 parentPos)
        {
            _damage = damage;
            _parentPos = parentPos;
        }
    }
}
