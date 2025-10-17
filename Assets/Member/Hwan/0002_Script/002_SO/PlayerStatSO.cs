using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "Scriptable Objects/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public float maxstamina;

    //increaseable
    public float decreaseValue;
    public float hp;
    public float nomalDmg;
    public float dashDmg;
    public float dashLength;
    public float moveSpeed;
    public float rotateSpeed;
}
