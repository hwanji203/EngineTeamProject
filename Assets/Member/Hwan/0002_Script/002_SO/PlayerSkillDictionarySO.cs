using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDictionarySO", menuName = "HwanSO/PlayerSkillDictionarySO")]
public class PlayerSkillDictionarySO : ScriptableObject
{
    [SerializeField] private PlayerSkill[] playerSkills;
    private Dictionary<PlayerSkillType, PlayerSkillSO> dictionary = new();
    public Dictionary<PlayerSkillType, PlayerSkillSO> Dictionary
    {
        get
        {
            if (dictionary.Count == playerSkills.Length) return dictionary;

            foreach (PlayerSkill playerSkill in playerSkills)
            {
                dictionary[playerSkill.MyType] = playerSkill.MySkill;
            }
            return dictionary;
        }
    }
}

[Serializable]
public class PlayerSkill
{
    [field: SerializeField] public PlayerSkillType MyType { get; private set; }
    [field: SerializeField] public PlayerSkillSO MySkill { get; private set; }
}