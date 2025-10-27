using System;
using UnityEngine;
using DG.Tweening;

public class AttackHitbox : MonoBehaviour
{

    [SerializeField] private Transform[] hitbox;

    private void TalmoBeam(int index, Vector2 dir, float duration)
    {
        Sequence s = DOTween.Sequence();
        
        gameObject.SetActive(true);
        transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        s.Join(hitbox[index].DOScaleX(1, duration));
        s.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            hitbox[index].localScale = new Vector3(0, hitbox[index].localScale.y, 1);
        });
    }
    
    public void ShowHitbox(Vector2 dir, float duration)
    {
        for (int i = 0; i < hitbox.Length; i++)
        {
            TalmoBeam(i,dir,duration);
        }
    }
}
