﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
            PlayerStats.playerHealth += 10;
            if(PlayerStats.playerHealth > PlayerStats.maxHP)
            {
                PlayerStats.playerHealth = PlayerStats.maxHP;
            }
            this.gameObject.SetActive(false);
        }
    }
}
