using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static float playerHealth = 100f;
    public static float maxHP = 100f;
    public static int gold;

    void start()
    {
       
        gold = 0;
    }


    private void takeDamage(int damage)
    {
        
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play death animation
        Destroy(gameObject);
    }
}
