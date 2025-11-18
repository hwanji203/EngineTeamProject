using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialInfoSO", menuName = "HwanSO/Info/TutorialInfoSO")]
public class TutorialInfoSO : ScriptableObject
{
    [field: SerializeField] public TutorialInfo[] TutorialInfos { get; private set; }
}

[Serializable]
public struct TutorialInfo
{
    [field: SerializeField] public string TutorialMessage { get; private set; }
    [SerializeField] private Vector2 fadePosOffset;
    [SerializeField] private Vector2 messagePosOffset;
    public Vector2 FadePosOffset
    {
        get
        {
            return new Vector2(fadePosOffset.x, fadePosOffset.y - 45);
        }
    }
    public Vector2 MessagePosOffset
    {
        get
        {
            return new Vector2(messagePosOffset.x, messagePosOffset.y - 45);
        }
    }
    [field: SerializeField] public Vector2 FadeScale { get; private set; }
    [field: SerializeField] public TutorialTarget TargetType { get; private set; }
}
