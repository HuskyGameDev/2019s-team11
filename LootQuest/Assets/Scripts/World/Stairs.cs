using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public string sceneToLoad;
    bool isOnStairs = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        isOnStairs = true;
        Debug.Log(isOnStairs);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnStairs = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isOnStairs == true)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
