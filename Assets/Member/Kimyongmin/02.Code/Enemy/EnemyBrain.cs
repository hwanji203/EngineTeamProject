using System;
using Member.Kimyongmin._02.Code.Enemy.State;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy
{
    public abstract class EnemyBrain : MonoBehaviour
    {
        protected EnemyStateMachine EnemyStateMachine;
        protected global::Enemy Enemy;

        protected void Awake()
        {
            Enemy = GetComponent<global::Enemy>();
            EnemyStateMachine = new EnemyStateMachine(this);
            
            EnemyStateMachine.AddState(StateType.Hit, new HitState(Enemy, EnemyStateMachine, "Hit"));
        }

        protected void Start()
        {
            Enemy.HealthSystem.OnHealthChanged += Hit;
        }

        public void Hit()
        {
            EnemyStateMachine.ChangeState(StateType.Hit);
        }

        protected void OnDestroy()
        {
            Enemy.HealthSystem.OnHealthChanged -= Hit;
        }
    }
}
