using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack
}

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private Dictionary<PlayerState, int> animationDictionary = new();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

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
}