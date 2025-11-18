using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialInfoSO", menuName = "HwanSO/Info/TutorialInfoSO")]
public class TutorialInfoSO : ScriptableObject
{
    [field: SerializeField] public StageInfo[] StageInfos { get; private set; }
}

[Serializable]
public struct StageInfo
{
    [field: SerializeField] public string TutorialMessage { get; private set; }
    [field: SerializeField] public Vector2 FadePosOffset { get; private set; }
    [field: SerializeField] public Vector2 MessagePosOffset { get; private set; }
    [field: SerializeField] public Vector2 FadeScale { get; private set; }
}
