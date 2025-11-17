using UnityEngine;

public interface ISkillCaster
{
    // 스킬 시전자의 Transform (위치, 회전 정보)
    Transform Transform { get; }
}