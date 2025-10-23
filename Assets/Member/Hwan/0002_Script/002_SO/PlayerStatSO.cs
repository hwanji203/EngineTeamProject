using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "HwanSO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public float maxStamina;
    public float decreaseStamina;
    public float hp;
    public float nomalDmg;
    public float dashDmg;
}
