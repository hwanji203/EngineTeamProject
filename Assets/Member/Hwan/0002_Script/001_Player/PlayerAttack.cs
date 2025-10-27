using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public event Action OnBasicAttack;
    public event Action OnDashAttack;
    // �����̽��ٰ� �������� ���, �ƴϸ� �⺻ ����
    // ���� �̺�Ʈ �߻�

    public void Attack(bool moving)
    {
        if (moving)
        {
            OnDashAttack?.Invoke();
            Debug.Log("��� ����");
        }
        else
        {
            OnBasicAttack?.Invoke();
            Debug.Log("�Ϲ� ����");
        }
    }
}
