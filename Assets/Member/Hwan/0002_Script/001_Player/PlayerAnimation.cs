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

    public event Action<PlayerAttackType> OnAttack;

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
        List<int> dontFalseList = new();

        ChangeList(state, dontFalseList);

        if (animator.GetBool(animationDictionary[PlayerState.Dash]) == true && state == PlayerState.Move)
        {
            ChangeList(PlayerState.StrMiddle, dontFalseList);
        }

        foreach (int hash in animationDictionary.Values)
        {
            if (dontFalseList.Any(value => hash == value) == false)
            {
                animator.SetBool(hash, false);
            }
        }
    }

    private void ChangeList(PlayerState state, List<int> dontFalseList)
    {
        dontFalseList.Clear();
        dontFalseList.Add(animationDictionary[state]);
        animator.SetBool(animationDictionary[state], true);
    }

    public void StartDash(PlayerAttackType type)
    {
        OnAttack?.Invoke(type);
    }
}