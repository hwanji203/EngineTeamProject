using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<UIType, IUI> uiDictionary = new Dictionary<UIType, IUI>();

    private void Start()
    {
        base.Awake();

        MonoBehaviour[] allComponents = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (MonoBehaviour comp in allComponents)
        {
            if (comp is IUI ui)
            {
                if (!uiDictionary.ContainsKey(ui.UIType))
                {
                    uiDictionary.Add(ui.UIType, ui);
                    ui.UIObject.SetActive(false);
                    ui.Initialize();
                }
            }
        }
    }

    public void OpenUI(UIType type)
    {
        if (uiDictionary.TryGetValue(type, out IUI ui))
        {
            ui.Open();
        }
    }

    public void CloseUI(UIType type)
    {
        if (uiDictionary.TryGetValue(type, out IUI ui))
        {
            ui.Close();
        }
    }
}
