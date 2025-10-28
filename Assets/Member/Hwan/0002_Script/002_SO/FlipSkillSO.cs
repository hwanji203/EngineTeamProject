using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;

[CreateAssetMenu(fileName = "FlipSkillSO", menuName = "HwanSO/Skills/FlipSkillSO")]
public class FlipSkillSO : PlayerSkillSO
{
    protected override void Skill(Transform playerTrn, float defaultDamage)
    {
        BoxAttack(playerTrn, defaultDamage);
    }
}
