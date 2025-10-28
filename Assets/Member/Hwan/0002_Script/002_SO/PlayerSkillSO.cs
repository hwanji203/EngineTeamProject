using Member.Kimyongmin._02.Code.Agent;
using System;
using UnityEngine;

public abstract class PlayerSkillSO : ScriptableObject
{
    [SerializeField] protected float damagePercent;
    [SerializeField] protected LayerMask enemyLayer;

    [SerializeField] private SkillEffect[] effects;

    [field: SerializeField] public Vector2 Range { get; private set; }

    public void AttackStart(Transform playerTrn, float defaultDamage)
    {
        //PrintEffect(playerTrn);

        Skill(playerTrn, defaultDamage);
    }
    protected void BoxAttack(Transform playerTrn, float defaultDamage)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerTrn.position, Range, playerTrn.eulerAngles.z, enemyLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.GetDamage(defaultDamage * damagePercent);
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

    protected abstract void Skill(Transform playerTrn, float defalutDamage);
}

[Serializable]
public class SkillEffect
{
    [field: SerializeField] public bool isParticle { get; private set; }
    [field: SerializeField] public GameObject effectPrefab { get; private set; }
}