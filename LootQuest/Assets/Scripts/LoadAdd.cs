using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadAdd : MonoBehaviour {

    //loads additive upon click
    public void LoadAddOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
