using System;
using Member.Kimyongmin._02.Code.Agent;
using Member.Kimyongmin._02.Code.Boss.SO;
using Unity.Behavior;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.Shark
{
    public class Shark : MonoBehaviour
    {
        [SerializeField] private SharkDataSO _sharkDataSO;
        private BehaviorGraphAgent _behaviorAgent;
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _behaviorAgent = GetComponent<BehaviorGraphAgent>();
            _healthSystem = GetComponent<HealthSystem>();

            _healthSystem.SetHealth(_sharkDataSO.HP);
            _behaviorAgent.SetVariableValue("HP", _healthSystem.Health);
        }
    }
}
