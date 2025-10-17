using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public event Action OnBasicAttack;
    public event Action OnDashAttack;
    // 스페이스바가 눌렸으면 대시, 아니면 기본 공격
    // 공격 이벤트 발생

    public void Attack(bool moving)
    {
        if (moving)
        {
            OnDashAttack?.Invoke();
            Debug.Log("대시 공격");
        }
        else
        {
            OnBasicAttack?.Invoke();
            Debug.Log("일반 공격");
        }
    }
}
