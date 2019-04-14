using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static float playerHealth;
    public static float maxHP;
    public static int gold;

    void start()
    {
        maxHP = 100;
        playerHealth = 0;
        gold = 0;
    }


    public void takeDamage(int damage)
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
