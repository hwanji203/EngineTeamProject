using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.SO
{
    [CreateAssetMenu(fileName = "SharkDataSO", menuName = "KimSO/SharkDataSO")]
    public class SharkDataSO : ScriptableObject
    {
        public float hp;
        public float speed;
        public float normalAttackDamage = 10;
        public float chargeDamage = 15;
        public float laserTickDamage = 2;
        
    }
}
