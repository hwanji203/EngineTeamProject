using UnityEngine;
using System.Linq;
using Unity.Behavior;

namespace Member.Kimyongmin._02.Code.Boss.Shark
{
    public class SharkFsm : MonoBehaviour
    {
        private BehaviorGraphAgent _behaviorAgent;

        public void SetUp(GameObject[] gameObjects)
        {
            _behaviorAgent = GetComponent<BehaviorGraphAgent>();

            _behaviorAgent.SetVariableValue("PatrolPoints", gameObjects.ToList());
        }
    }
}
