using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkillSO", menuName = "HwanSO/Skills/DashSkillSO")]
public class DashSkillSO : PlayerSkillSO
{
    public override IEnumerator AttackStart(Transform playerTrn, float defaultDamage)
    {
        detectedCollider.Clear();
        CheckBox(playerTrn, defaultDamage);
        VFXManager.Instance.Play(VFXType.DashBoost, playerTrn.position + playerTrn.right * 1.5f, Quaternion.Euler(0, 0, playerTrn.eulerAngles.z + 90), null);
        VFXManager.Instance.Play(VFXType.DashAttack, playerTrn.position + playerTrn.right * 1.15f, playerTrn.rotation, playerTrn);
        for (int i = 0; i < attackCount - 1; i++)
        {
            yield return new WaitForSeconds(AttackTime / attackCount - 1);
            CheckBox(playerTrn, defaultDamage);
        }
    }
}
