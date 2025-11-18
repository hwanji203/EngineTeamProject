using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class DeadState : EnemyState
    {
        private float _currentDeadTime = 0f;
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
            _currentDeadTime += Time.deltaTime;
            if (_currentDeadTime > 5)
            {
                Enemy.DeadGameobject();
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
