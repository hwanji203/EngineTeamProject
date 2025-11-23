using Member.Kimyongmin._02.Code.Agent;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Member.Kimyongmin._02.Code.Enemy;
using NUnit.Framework.Constraints;

public abstract class PlayerSkillSO : ScriptableObject
{
    [SerializeField] protected float damagePercent;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected int attackCount;
    [SerializeField] private SkillEffect[] effects;
    [field: SerializeField] public float AttackTime { get; private set; }
    [SerializeField] private Vector2 range;
    [SerializeField] private Vector3 offset;

    public Vector3 RealOffSet { get => new Vector3(offset.y, offset.x);}
    public Vector3 RealRange { get => new Vector3(range.y, range.x);}

    protected List<Collider2D> detectedCollider = new();

    public abstract IEnumerator AttackStart(Transform playerTrn, float defaultDamage);

    public event Action<bool> OnAttack;

    protected void CheckBox(Transform playerTrn, float defaultDamage, PlayerAttackType attackType)
    {
        Vector2 attackPoint = playerTrn.position + Quaternion.Euler(0, 0, playerTrn.eulerAngles.z) * RealOffSet;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPoint, RealRange, playerTrn.eulerAngles.z);
        foreach (Collider2D collider in colliders)
        {
            if (detectedCollider.Contains(collider) == true) continue;
            if (collider.TryGetComponent(out IAgentable enemy))
            {
                detectedCollider.Add(collider);
                AttackReturnType returnType = enemy.GetDamage(defaultDamage * damagePercent, attackType);
                SoundManager.Instance.Play(attackType switch
                {
                    PlayerAttackType.Dash => SFXSoundType.EnemyDashDamaged,
                    PlayerAttackType.Flip => SFXSoundType.EnemyFlipDamaged,
                    _ => SFXSoundType.EnemyDashDamaged
                });
                if (returnType == AttackReturnType.None) return;
                OnAttack?.Invoke(returnType == AttackReturnType.Counter);
            }
        }
    }
}

[Serializable]
public class SkillEffect
{
    [field: SerializeField] public bool isParticle { get; private set; }
    [field: SerializeField] public GameObject effectPrefab { get; private set; }
}