using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [ContextMenu("difm")]
    private void AA()
    {
        UIManager.Instance.OpenUI(UIType.TutorialUI);
    }
}
