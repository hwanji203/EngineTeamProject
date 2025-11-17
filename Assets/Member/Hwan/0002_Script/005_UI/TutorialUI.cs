using UnityEngine;

public class TutorialUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType => UIType.TutorialUI;

    public void Close()
    {
        //throw new System.NotImplementedException();
    }

    public void Initialize()
    {
        //throw new System.NotImplementedException();
    }

    public void Open()
    {
        //throw new System.NotImplementedException();
    }
}
