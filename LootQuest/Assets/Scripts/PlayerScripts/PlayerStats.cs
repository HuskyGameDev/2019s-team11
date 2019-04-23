using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public static float playerHealth = 100f;
    public static float maxHP = 100f;
    public static int gold = 0;

    void start()
    {
        playerHealth = 100f;
        maxHP = 100f;
        gold = 0;
    }


    public void takeDamage(int damage)
    {

        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play death animation
        SceneManager.LoadScene("Overworld");
        playerHealth = 100f;
    }
}
