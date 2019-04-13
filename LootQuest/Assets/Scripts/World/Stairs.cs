using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void Update()
    {
        
    }
}
