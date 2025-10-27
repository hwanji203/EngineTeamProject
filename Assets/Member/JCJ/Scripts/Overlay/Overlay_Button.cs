using System;
using TMPro;
using UnityEngine;

public abstract class Overlay_Button:MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject panel;
    public ButtonManager buttonManager;
    public Overlay_Button myOverlayButton;

    public virtual void UseButton()
    {
        buttonManager.ClickButon(myOverlayButton);
        text.fontSize = 96;
        panel.SetActive(true);
    }

    public virtual void OutButton()
    {
        text.fontSize = 48;
        panel.SetActive(false);
    }
}
