using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    //loads the scene based on the provided index
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene (sceneIndex);
    }
}
