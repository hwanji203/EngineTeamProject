using DG.Tweening;
using UnityEngine;

public class FlipMovement : Movement
{
    public override void Move(MoveValue moveValue, Vector2 mousePos)
    {
        moveValue.Trn.DOBlendableRotateBy(new Vector3(0, 0, 360f),
            moveValue.MovementSO.skillDictionarySO.Dictionary[PlayerSkillType.Flip].AttackTime, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCirc);
    }
}
