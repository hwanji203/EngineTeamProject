using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Member.Hwan._0002_Script._002_SO;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerSkillDictionarySO skillDictionarySO;
    public PlayerStatSo StatSO { get; set; }

    // �����̽��ٰ� �������� ���, �ƴϸ� �⺻ ����
    // ���� �̺�Ʈ �߻�
    public Coroutine DashCoolCoroutine { get; private set; }
    public Coroutine FlipCoolCoroutine { get; private set; }

    private List<PlayerSkillType> currentEquippedSkills = new();

    private void Start()
    {
        //�ӽ� (���߿� json���� �ҷ��ò�)
        AddSkill(PlayerSkillType.Flip);
    }

    private void Update()
    {
        foreach (PlayerSkillType skillType in Enum.GetValues(typeof(PlayerSkillType)))
        {
            if (skillDictionarySO.Dictionary.TryGetValue(skillType, out PlayerSkillSO skillSO))
            {
                DrawRangeManager.Instance.DrawBox(skillSO.Range, transform.eulerAngles.z, transform.position);
            }
        }
    }

    public void AddSkill(PlayerSkillType skillType)
    {
        if (currentEquippedSkills.Count >= StatSO.maxSkillCount) return;
        currentEquippedSkills.Add(skillType);
    }

    public void Attack(PlayerAttackType type)
    {
        if (type == PlayerAttackType.Dash)
        {
            skillDictionarySO.Dictionary[PlayerSkillType.Dash].AttackStart(transform, StatSO.defaultDmg);
        }
        else
        {
            foreach (PlayerSkillType skillType in currentEquippedSkills)
            {
                skillDictionarySO.Dictionary[skillType].AttackStart(transform, StatSO.defaultDmg);
            }
        }
    }

    public void StartAttack(PlayerAttackType attackType)
    {
        switch (attackType)
        {
            case PlayerAttackType.Dash:
                DashCoolCoroutine = StartCoroutine(AttackCool(PlayerAttackType.Dash));
                break;
            case PlayerAttackType.Flip:
                FlipCoolCoroutine = StartCoroutine(AttackCool(PlayerAttackType.Flip));
                break;
        }
    }

    private IEnumerator AttackCool(PlayerAttackType attackType)
    {
        switch (attackType)
        {
            case PlayerAttackType.Dash:
                yield return new WaitForSeconds(StatSO.dashCool);
                DashCoolCoroutine = null;
                break;
            case PlayerAttackType.Flip:
                yield return new WaitForSeconds(StatSO.flipCool);
                FlipCoolCoroutine = null;
                break;
        }
    }
}
