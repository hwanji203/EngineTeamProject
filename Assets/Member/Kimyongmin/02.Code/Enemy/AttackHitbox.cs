using System;
using UnityEngine;
using DG.Tweening;

public class AttackHitbox : MonoBehaviour
{

    [SerializeField] private Transform hitbox;

    public void ShowHitbox(Vector2 dir, float duration)
    {
        Sequence s = DOTween.Sequence();
        
        gameObject.SetActive(true);
        transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        s.Join(hitbox.DOScaleX(1, duration));
        s.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            hitbox.localScale = new Vector3(0, hitbox.localScale.y, 1);
        });
    }
}
