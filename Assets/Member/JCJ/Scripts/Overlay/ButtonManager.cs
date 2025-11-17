    using System;
    using UnityEngine;

    public class ButtonManager : MonoBehaviour
    {
        [SerializeField]private Overlay_Button[] buttons;

        public void ClickButon(Overlay_Button overlayButton)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != overlayButton)
                {
                    buttons[i].OutButton();
                }
            }
        }
        
    }
