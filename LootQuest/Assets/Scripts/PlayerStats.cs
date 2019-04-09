using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static float playerHealth;
    public static float maxHP = 100;
    public static int gold = 0;
    private float hitDelay = 0.5f;
    public GameObject player;
    private bool playerHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHit)
        {

        }
    }

    private void applyDamage(int damage)
    {
        
        playerHealth = playerHealth - damage;
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play death animation
        Destroy(player);
    }
}
