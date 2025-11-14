using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FlipSkillSO", menuName = "HwanSO/Skills/FlipSkillSO")]
public class FlipSkillSO : PlayerSkillSO
{
    public override IEnumerator AttackStart(Transform playerTrn, float defaultDamage)
    {
        Debug.Log("df");
        detectedCollider.Clear();
        CheckBox(playerTrn, defaultDamage);

        for (int i = 0; i < attackCount - 1; i++)
        {
            yield return new WaitForSeconds(AttackTime / attackCount - 1);
            Debug.Log("sdfsf");
            CheckBox(playerTrn, defaultDamage);
        }
    }
}
