using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.SO
{
    [CreateAssetMenu(fileName = "SharkDataSO", menuName = "KimSO/SharkDataSO")]
    public class SharkDataSO : ScriptableObject
    {
        [field:SerializeField] public float Hp {get; private set;}
        [field:SerializeField] public float Speed {get; private set;}
        [field:SerializeField] public float NormalAttackDamage {get; private set;}
        [field:SerializeField] public float ChargeDamage {get; private set;}
        [field:SerializeField] public float LaserTickDamage {get; private set;}

        [field: SerializeField] public float MinSkillCool {get; private set;}
        [field: SerializeField] public float MaxSkillCool {get; private set;}

    }
}
