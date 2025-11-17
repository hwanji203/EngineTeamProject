using DG.Tweening;
using UnityEngine;

public class PlayerBlackEyeMove : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;

    public void Move(Vector2 mousePos)
    {
        var parent = transform.parent;
        Vector2 center = (Vector2)parent.position;

        Vector2 dir = mousePos - center;
        Vector2 offset = (dir.sqrMagnitude > radius) ? dir.normalized * radius : dir;

        Vector3 targetWorld = (Vector3)(center + offset);
        Vector3 targetLocal = parent.InverseTransformPoint(targetWorld);

        transform.DOKill();
        transform.DOLocalMove(targetLocal, speed)
                 .SetEase(Ease.OutQuad)
                 .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    public void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

}
