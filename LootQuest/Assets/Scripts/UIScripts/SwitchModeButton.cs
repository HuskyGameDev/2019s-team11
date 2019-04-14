using UnityEngine;
using System.Collections;

public class SwitchModeButton : MonoBehaviour
{
    //creates a toggle button on the menu
    public void SwitchPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}
