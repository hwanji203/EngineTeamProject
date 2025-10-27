using System;
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public event Action OnBasicAttack;
    public event Action OnDashAttack;
    // �����̽��ٰ� �������� ���, �ƴϸ� �⺻ ����
    // ���� �̺�Ʈ �߻�
    public Coroutine DashCoolCoroutine { get; private set; }
    public Coroutine FlipCoolCoroutine { get; private set; }

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
                yield return new WaitForSeconds(StatSO.dashCool);
                DashCoolCoroutine = null;
                break;
            case PlayerAttackType.Flip:
                yield return new WaitForSeconds(StatSO.flipCool);
                FlipCoolCoroutine = null;
                break;
        }
    }
}
