using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerSkillDictionarySO skillDictionarySO;

    public Coroutine DashCoolCoroutine { get; private set; }
    public Coroutine FlipCoolCoroutine { get; private set; }

    private List<PlayerSkillType> currentEquippedSkills = new();
    private PlayerStatSO statSO;

    private Dictionary<PlayerSkillType, Coroutine> attackCoroutineDictionary = new();

    [SerializeField] private PlayerSkillType seeSkill;
    public event Action<bool> OnAttack;

    private void Update()
    {
        PlayerSkillSO skill = skillDictionarySO.Dictionary[seeSkill];

        //DrawRangeManager.Instance.DrawBox(skill.RealRange, transform.eulerAngles.z, transform.position, skill.RealOffSet);
    }

    public void AddSkill(PlayerSkillType skillType)
    {
        if (currentEquippedSkills.Count >= statSO.MaxSkillCount) return;
        currentEquippedSkills.Add(skillType);
    }

    public void Attack(PlayerAttackType type)
    {
        if (type == PlayerAttackType.Dash)
        {
            attackCoroutineDictionary[PlayerSkillType.Dash] = StartCoroutine(skillDictionarySO.Dictionary[PlayerSkillType.Dash].AttackStart(transform, statSO.DefaultDmg));
        }
        else
        {
            foreach (PlayerSkillType skillType in currentEquippedSkills)
            {
                attackCoroutineDictionary[skillType] = StartCoroutine(skillDictionarySO.Dictionary[skillType].AttackStart(transform, statSO.DefaultDmg));
            }
        }
        StartAttack(type);
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
                yield return new WaitForSeconds(statSO.DashCool);
                DashCoolCoroutine = null;
                break;
            case PlayerAttackType.Flip:
                yield return new WaitForSeconds(statSO.FlipCool);
                FlipCoolCoroutine = null;
                break;
        }
    }

    public void AttackCancel()
    {
        foreach (Coroutine coroutine in attackCoroutineDictionary.Values)
        {
            StopCoroutine(coroutine);
        }
    }

    public void Initialize(PlayerStatSO statSO)
    {
        this.statSO = statSO; 
        
        AddSkill(PlayerSkillType.Flip);

        foreach (PlayerSkillSO skillSO in skillDictionarySO.Dictionary.Values)
        {
            skillSO.OnAttack += (isCounter) => OnAttack?.Invoke(isCounter);
        }
    }
}
