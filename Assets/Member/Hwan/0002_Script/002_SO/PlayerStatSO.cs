using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "HwanSO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public float maxStamina;
    public float swimStamina;
    public float dashStamina;
    public float maxHp;
    public float defaultDmg;
    public float flipCool;
    public float dashCool;
    public int maxSkillCount;
}
