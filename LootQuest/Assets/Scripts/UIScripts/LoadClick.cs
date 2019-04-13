using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadClick : MonoBehaviour {

    public GameObject loadingImage;

    //loads the scene of the level on click
    public void LoadScene(int level)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }
}
