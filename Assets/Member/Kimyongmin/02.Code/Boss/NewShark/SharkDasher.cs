using System.Collections;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkDasher : MonoBehaviour
    {
        public IEnumerator ChargeReady(Vector3 target, SharkMovement sharkMovement)
        {
            float duration = 1f;
            float t = 0;

            while (t < duration)
            {
                t += Time.deltaTime;
                
                sharkMovement.RbCompo.linearVelocity = -target * 1;
                yield return new WaitForFixedUpdate();
            }
        }
        
        public IEnumerator ChargeAttack(Vector3 target, SharkMovement sharkMovement)
        {
            float duration = 5;
            float t = 0;

            while (t < duration)
            {
                t += Time.deltaTime;
                
                sharkMovement.RbCompo.linearVelocity = target * 20;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
