using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkillSO", menuName = "HwanSO/Skills/DashSkillSO")]
public class DashSkillSO : PlayerSkillSO
{
    protected override void Skill(Transform playerTrn, float defaultDamage)
    {
        BoxAttack(playerTrn, defaultDamage);
    }
}
