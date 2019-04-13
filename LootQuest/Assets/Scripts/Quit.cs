using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

    //quits the menu on click
    public void QuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

}
