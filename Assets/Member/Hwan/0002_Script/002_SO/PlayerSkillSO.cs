using Member.Kimyongmin._02.Code.Agent;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerSkillSO : ScriptableObject
{
    [SerializeField] protected float damagePercent;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] private Vector3 offset;
    [SerializeField] protected int attackCount;
    [SerializeField] private SkillEffect[] effects;

    [field: SerializeField] public Vector2 Range { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
    public Vector3 RealOffSet { get => new Vector3(offset.y, offset.x);}

    protected List<Collider2D> detectedCollider;

    public abstract IEnumerator AttackStart(Transform playerTrn, float defaultDamage);

    protected void CheckBox(Transform playerTrn, float defaultDamage)
    {
        Vector2 attackPoint = playerTrn.position + Quaternion.Euler(0, 0, playerTrn.eulerAngles.z) * RealOffSet;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPoint, Range, playerTrn.eulerAngles.z);

        foreach (Collider2D collider in colliders)
        {
            if (detectedCollider.Contains(collider) == true) continue;
            if (collider.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.GetDamage(defaultDamage * damagePercent);
                detectedCollider.Add(collider);
            }
        }
    }

    private void PrintEffect(Transform playerTrn)
    {
        foreach (SkillEffect skillEffect in effects)
        {
            GameObject effect = Instantiate(skillEffect.effectPrefab);
            effect.transform.position = playerTrn.position;
            effect.transform.rotation = playerTrn.rotation;

            if (skillEffect.isParticle == true)
            {
                effect.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                effect.GetComponent<Animator>().SetTrigger("Start");
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