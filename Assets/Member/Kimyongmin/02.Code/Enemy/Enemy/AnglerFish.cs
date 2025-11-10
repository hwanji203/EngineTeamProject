using System.Collections;
using Member.Kimyongmin._02.Code.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class AnglerFish : global::Enemy, IHitface
    {
        public override void Attack()
        {
            IsAttack = true;
            AgentMovement.IsDashing = true;
            ResetCooltime();
            AgentMovement.RbCompo.linearVelocity = GetTarget() + new Vector2(GetTarget().x > 0 ? 1 : -1, 0) * 1f;
            StartCoroutine(HitPanJeong());
        }

        public override void Death()
        {
            Destroy(gameObject);
        }

        public float panjeongTime { get; set; }
        public float panjeongDuration { get; set; }
        
        public IEnumerator HitPanJeong()
        {
            yield return new WaitForSeconds(0.5f);
            IsAttack = false;
            AgentMovement.IsDashing = false;
        }
    }
}
