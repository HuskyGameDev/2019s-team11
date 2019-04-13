using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public int attackRange;
    public int chaseRange;
    public int attackPower;
    public Vector2 spawnPoint;


    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage... Bat health =" + health);
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("should die");
        Destroy(gameObject);
    }
}
