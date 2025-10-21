using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack,
    StrMiddle
}

public enum PlayerAttackType
{
    Default,
    Dash
}

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private Dictionary<PlayerState, int> animationDictionary = new();

    public event Action<PlayerAttackType, bool> OnAttackChange;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animationDictionary.Add(PlayerState.Idle, Animator.StringToHash("Idle"));
        animationDictionary.Add(PlayerState.Move, Animator.StringToHash("Move"));
        animationDictionary.Add(PlayerState.Dash, Animator.StringToHash("Dash"));
        animationDictionary.Add(PlayerState.Attack, Animator.StringToHash("Attack"));
        animationDictionary.Add(PlayerState.StrMiddle, Animator.StringToHash("StrMiddle"));
    }

    public void ChangeAnimation(PlayerState state)
    {
        int trueStateHash;

        trueStateHash = animationDictionary[state];

        if (animator.GetBool(animationDictionary[PlayerState.Dash]) == true && state == PlayerState.Move)
        {
            trueStateHash = animationDictionary[PlayerState.StrMiddle];
        }

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

    public void StartAttack(PlayerAttackType type)
    {
        OnAttackChange?.Invoke(type, true);
    }
    public void EndAttack(PlayerAttackType type)
    {
        OnAttackChange?.Invoke(type, false);
    }
}