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
        playerHealth = 100f;
        maxHP = 100f;
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
