using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack
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

    public event Action<PlayerAttackType> OnAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animationDictionary.Add(PlayerState.Idle, Animator.StringToHash("Idle"));
        animationDictionary.Add(PlayerState.Move, Animator.StringToHash("Move"));
        animationDictionary.Add(PlayerState.Dash, Animator.StringToHash("Dash"));
        animationDictionary.Add(PlayerState.Attack, Animator.StringToHash("Attack"));
    }

    public void ChangeAnimation(PlayerState state)
    {
        foreach (int hash in animationDictionary.Values)
        {
            if (hash != animationDictionary[state])
            {
                animator.SetBool(hash, false);
            }
        }

        animator.SetBool(animationDictionary[state], true);
    }

    public void StartDash(PlayerAttackType type)
    {
        OnAttack?.Invoke(type);
    }
}