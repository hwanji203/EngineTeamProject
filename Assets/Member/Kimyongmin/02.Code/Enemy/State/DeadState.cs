using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class DeadState : EnemyState
    {
        public DeadState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            SpriteRenderer spriteRenderer = Enemy.transform.GetComponentInDirectChildren<SpriteRenderer>();
            spriteRenderer.DOColor(new Color(0, 0, 0), 3f);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
