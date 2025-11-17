using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkillSO", menuName = "HwanSO/Skills/DashSkillSO")]
public class DashSkillSO : PlayerSkillSO
{
    public override IEnumerator AttackStart(Transform playerTrn, float defaultDamage)
    {
        detectedCollider.Clear();
        CheckBox(playerTrn, defaultDamage);
        VFXManager.Instance.Play(VFXType.DashAttack, playerTrn.position, playerTrn.rotation);
        VFXManager.Instance.Play(VFXType.DashBoost, playerTrn.position, playerTrn.rotation);
        for (int i = 0; i < attackCount - 1; i++)
        {
            yield return new WaitForSeconds(AttackTime / attackCount - 1);
            CheckBox(playerTrn, defaultDamage);
        }
    }
}
