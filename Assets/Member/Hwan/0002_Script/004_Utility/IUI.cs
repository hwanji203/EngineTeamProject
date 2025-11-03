using Unity.VisualScripting;
using UnityEngine;

public interface IUI
{
    public UIType UIType { get; }
    public abstract void Initialize();
    public abstract void Open();
    public abstract void Close();
}
