using UnityEngine;

namespace Member.Hwan._0002_Script._002_SO
{
    [CreateAssetMenu(fileName = "PlayerStatSo", menuName = "HwanSO/PlayerStatSo")]
    public class PlayerStatSo : ScriptableObject
    {
        public float maxStamina;
        public float swimStamina;
        public float dashStamina;
        public float defaultDmg;
        public float flipCool;
        public float dashCool;
        public int maxSkillCount;
    }
}
