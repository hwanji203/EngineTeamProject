using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;
using System.Collections;
using System;

public class TutorialEnemy : MonoBehaviour
{
    [SerializeField] private float triggerDis;
    [SerializeField] private float attackTutoWaitTime;
    private UIManager uiManager;
    private Transform playerTrn;
    private HealthSystem healthSystem;
    private Enemy enemy;

    private bool distanceTriggered = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        uiManager =  UIManager.Instance;
        playerTrn = GameManager.Instance.Player.transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthChanged += Damaged;
        healthSystem.OnDeath += () => uiManager.OpenUI(UIType.TutorialUI); // 튜토 끝
    }

    private void Update()
    {
        if (distanceTriggered == false && triggerDis > (playerTrn.position - transform.position).magnitude)
        {
            uiManager.OpenUI(UIType.TutorialUI); // 움직일 수 있음
            distanceTriggered = true;
            StartCoroutine(WaitAttackTutorial());
        }
    }

    private IEnumerator WaitAttackTutorial()
    {
        yield return new WaitForSeconds(attackTutoWaitTime);
        uiManager.OpenUI(UIType.TutorialUI); //회전할 수 있음
    }

    private void Damaged()
    {
        if (healthSystem.GetHealthPercent() < 0.55f && enemy.IsInvincibility == false)
        {
            uiManager.OpenUI(UIType.TutorialUI); // 대시할 수 있지만 카운터 아니면 데미지 입힐 수 없음
            enemy.IsInvincibility = true;
        }
    }
}
