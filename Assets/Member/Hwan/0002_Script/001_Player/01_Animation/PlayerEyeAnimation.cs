using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeAnimation : MonoBehaviour
{
    [SerializeField] private GameObject blackEye;
    [SerializeField] private float blinkPercent = 0.05f;

    private Dictionary<PlayerEyeState, int> animationDictionary = new();
    private Animator animator;
    private bool blinking = false;

    public void Initialize()
    {
        animator = GetComponent<Animator>();

        animationDictionary.Add(PlayerEyeState.Idle, Animator.StringToHash("Idle"));
        animationDictionary.Add(PlayerEyeState.Attack, Animator.StringToHash("Attack"));
        animationDictionary.Add(PlayerEyeState.Hit, Animator.StringToHash("Hit"));
        animationDictionary.Add(PlayerEyeState.Dead, Animator.StringToHash("Dead"));
        animationDictionary.Add(PlayerEyeState.Blink, Animator.StringToHash("Blink"));
    }

    public void ChangeAnimation(PlayerState state)
    {
        PlayerEyeState eyeState = PlayerEyeState.None;

        switch (state)
        {
            case PlayerState.Dash:
                eyeState = PlayerEyeState.Attack;
                break;
            case PlayerState.Flip:
                eyeState = PlayerEyeState.Attack;
                break;
            case PlayerState.Idle:
                eyeState = PlayerEyeState.Idle;
                break;
            case PlayerState.WaitForAttack:
                eyeState = PlayerEyeState.Idle;
                break;
            case PlayerState.Move:
                eyeState = PlayerEyeState.Idle;
                break;
            case PlayerState.ZeroStamina:
                eyeState = PlayerEyeState.Dead;
                break;
            case PlayerState.Hit:
                eyeState = PlayerEyeState.Hit;
                break;
        }

        int trueStateHash = 0;

        if (!blinking && eyeState == PlayerEyeState.Idle && Random.Range(0f, 1f) < blinkPercent) blinking = true;
        if (eyeState == PlayerEyeState.Idle && blinking == true)
        {
            trueStateHash = animationDictionary[PlayerEyeState.Blink];
        }
        else
        {
            trueStateHash = animationDictionary[eyeState];
        }

        blackEye.SetActive(eyeState == PlayerEyeState.Idle && blinking == false);

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

    public void EndBlink()
    {
        blinking = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.right, 0.4f);
    }
}
