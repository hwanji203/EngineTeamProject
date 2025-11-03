using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class HitState : EnemyState
    {
        private float _currentTime = 0;
        public HitState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.HealthSystem.OnHealthChanged += ResetStun;
            Enemy.AgentMovement.IsHit = true;
            ResetStun();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            
            if (_currentTime > 1f)
            {
                EnemyStateMachine.ChangeState(StateType.Idle);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.AgentMovement.IsHit = false;
            Enemy.HealthSystem.OnHealthChanged -= ResetStun;
        }

        private void ResetStun()
        {
            DOTween.Kill(Enemy.gameObject);
            _currentTime = 0;
        }
    }
}
