using Unity.VisualScripting;
using UnityEngine;

public interface IUI
{
    public GameObject UIObject { get; }
    public UIType UIType { get; }
    public void Initialize();
    public void Open();
    public void Close();
}
