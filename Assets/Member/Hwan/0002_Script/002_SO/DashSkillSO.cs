using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkillSO", menuName = "HwanSO/Skills/DashSkillSO")]
public class DashSkillSO : PlayerSkillSO
{
    public override IEnumerator AttackStart(Transform playerTrn, float defaultDamage)
    {
        detectedCollider.Clear();

        for (int i = 0; i < attackCount; i++)
        {
            CheckBox(playerTrn, defaultDamage);

            if (i + 1 == attackCount) break;
            yield return new WaitForSeconds(AttackTime / attackCount);
        }
    }
}
