using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FlipSkillSO", menuName = "HwanSO/Skills/FlipSkillSO")]
public class FlipSkillSO : PlayerSkillSO
{
    public override IEnumerator AttackStart(Transform playerTrn, float defaultDamage)
    {
        detectedCollider.Clear();
        CheckBox(playerTrn, defaultDamage);
        VFXManager.Instance.Play(VFXType.Swing, playerTrn.position + RealOffSet, playerTrn.rotation, playerTrn);
        for (int i = 0; i < attackCount - 1; i++)
        {
            yield return new WaitForSeconds(AttackTime / (attackCount - 1));
            CheckBox(playerTrn, defaultDamage);
        }
    }
}
