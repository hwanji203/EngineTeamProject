using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private Dictionary<PlayerState, int> animationDictionary = new();

    public event Action<PlayerAttackType> OnAttackStart;
    public event Action OnAttackEnd;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animationDictionary.Add(PlayerState.Idle, Animator.StringToHash("Idle"));
        animationDictionary.Add(PlayerState.Move, Animator.StringToHash("Move"));
        animationDictionary.Add(PlayerState.Dash, Animator.StringToHash("Dash"));
        animationDictionary.Add(PlayerState.Flip, Animator.StringToHash("Attack"));
    }

    public void ChangeAnimation(PlayerState state)
    {
        int trueStateHash;

        trueStateHash = animationDictionary[state];

        foreach (int hash in animationDictionary.Values)
        {
            if (trueStateHash != hash)
            {
                animator.SetBool(hash, false);
            }
            else
            {
                animator.SetBool(hash, true);
            }
        }
    }

    public bool CanAttack()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return !(info.IsName("PlayerDashLast") == true 
            || info.IsName("PlayerFlip") == true || info.IsName("PlayerDashFir") == true);
    }

    public void StartAttack(PlayerAttackType type)
    {
        OnAttackStart?.Invoke(type);
    }
    public void EndAttack()
    {
        OnAttackEnd?.Invoke();
        ChangeAnimation(PlayerState.Idle);
    }
}