using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public event Action OnBasicAttack;
    public event Action OnDashAttack;
    // �����̽��ٰ� �������� ���, �ƴϸ� �⺻ ����
    // ���� �̺�Ʈ �߻�

    public void Attack(PlayerAttackType type)
    {
        if (type == PlayerAttackType.Dash)
        {
            OnDashAttack?.Invoke();
        }
        else
        {
            OnBasicAttack?.Invoke();
        }
    }
}
