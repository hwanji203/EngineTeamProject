using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "HwanSO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public float maxStamina;
    public float swimStamina;
    public float dashStamina;
    public float hp;
    public float nomalDmg;
    public float dashDmg;
}
