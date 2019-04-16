using UnityEngine;
using System.Collections;

public class QuitClick : MonoBehaviour {

    //quits the menu on click
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

}
